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
    public class DetalleComprasController : Controller
    {
        private readonly ContextoBD _context;

        public DetalleComprasController(ContextoBD context)
        {
            _context = context;
        }

        // GET: DetalleCompras
        public async Task<IActionResult> Index()
        {
            var contextoBD = _context.DetalleCompra.Include(d => d.Productos).Include(d => d.Proveedores);
            return View(await contextoBD.ToListAsync());
        }

        // GET: DetalleCompras/Create
        [Authorize(Roles = "Supervisor,Administrador,Empleado")]
        public IActionResult Create()
        {
            ViewData["Producto_ID"] = new SelectList(_context.Productos, "Producto_ID", "Producto_Nombre");
            ViewData["Proveedores_ID"] = new SelectList(_context.Proveedores, "Proveedores_ID", "RazonSocial");
            return View();
        }

        // POST: DetalleCompras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DetalleCompra_ID,Proveedores_ID,Producto_ID,PrecioCompra,PrecioVenta,Cantidad,Total")] DetalleCompra detalleCompra)
        {
            var producto = _context.Productos.FirstOrDefault(x => x.Producto_ID == detalleCompra.Producto_ID);
            if (ModelState.IsValid)
            {
                if (producto.Stock < detalleCompra.Cantidad)
                {
                    /*<div class="alert alert-success">No hay suficiente stock</div>*/
                }
                if (producto != null)
                {
                    producto.Stock += detalleCompra.Cantidad;  
                    _context.Update(producto);
                }

                detalleCompra.Total = detalleCompra.Cantidad * detalleCompra.PrecioCompra;
                producto.Total = producto.Total + detalleCompra.Total;
                producto.Precio_Compra = producto.Total/producto.Stock;
                producto.Precio_Venta = detalleCompra.PrecioVenta;
                detalleCompra.FechaCompra = DateTime.Now;
                _context.Add(detalleCompra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Producto_ID"] = new SelectList(_context.Productos, "Producto_ID", "Producto_Nombre", detalleCompra.Producto_ID);
            ViewData["Proveedores_ID"] = new SelectList(_context.Proveedores, "Proveedores_ID", "RazonSocial", detalleCompra.Proveedores_ID);
            return View(detalleCompra);
        }

        // GET: DetalleCompras/Delete/5
        [Authorize(Roles = "Supervisor,Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleCompra = await _context.DetalleCompra
                .Include(d => d.Productos)
                .Include(d => d.Proveedores)
                .FirstOrDefaultAsync(m => m.DetalleCompra_ID == id);
            if (detalleCompra == null)
            {
                return NotFound();
            }

            return View(detalleCompra);
        }

        // POST: DetalleCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalleCompra = await _context.DetalleCompra.FindAsync(id);
            _context.DetalleCompra.Remove(detalleCompra);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleCompraExists(int id)
        {
            return _context.DetalleCompra.Any(e => e.DetalleCompra_ID == id);
        }
    }
}
