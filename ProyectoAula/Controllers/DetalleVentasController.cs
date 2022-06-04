using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoAula.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoAula.Controllers
{
    [Authorize]
    public class DetalleVentasController : Controller
    {
        private readonly ContextoBD _context;

        public DetalleVentasController(ContextoBD context)
        {
            _context = context;
        }

        // GET: DetalleVentas
        public async Task<IActionResult> Index()
        {
            var contextoBD = _context.DetalleVentas.Include(d => d.Productos);
            return View(await contextoBD.ToListAsync());
        }

        // GET: DetalleVentas/Create
        [Authorize(Roles = "Supervisor,Administrador,Empleado")]
        public IActionResult Create()
        {
            ViewData["Producto_ID"] = new SelectList(_context.Productos, "Producto_ID", "Producto_Nombre");
            return View();
        }

        // POST: DetalleVentas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DetalleVenta_ID,Producto_ID,PrecioVenta,Cantidad,Total")] DetalleVenta detalleVentas)
        {
            var producto = _context.Productos.FirstOrDefault(x => x.Producto_ID == detalleVentas.Producto_ID);
            if (ModelState.IsValid)
            {
                if (producto != null)
                {
                    producto.Stock -= detalleVentas.Cantidad;
                    producto.Total = producto.Stock * producto.Precio_Compra;
                    _context.Update(producto);
                }

                detalleVentas.PrecioVenta = producto.Precio_Venta;
                detalleVentas.Total = detalleVentas.Cantidad * detalleVentas.PrecioVenta;
                detalleVentas.FechaVenta = DateTime.Now;
                _context.Add(detalleVentas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Producto_ID"] = new SelectList(_context.Productos, "Producto_ID", "Producto_Nombre", detalleVentas.Producto_ID);
            return View(detalleVentas);
        }

        // GET: DetalleVentas/Delete/5
        [Authorize(Roles = "Supervisor,Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleVentas = await _context.DetalleVentas
                .Include(d => d.Productos)
                .FirstOrDefaultAsync(m => m.DetalleVenta_ID == id);
            if (detalleVentas == null)
            {
                return NotFound();
            }

            return View(detalleVentas);
        }

        // POST: DetalleVentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalleVentas = await _context.DetalleVentas.FindAsync(id);
            _context.DetalleVentas.Remove(detalleVentas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleVentaExists(int id)
        {
            return _context.DetalleVentas.Any(e => e.DetalleVenta_ID == id);
        }
    }
}
