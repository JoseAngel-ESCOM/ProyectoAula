using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoAula.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace ProyectoAula.Controllers
{
    [Authorize]
    public class ProductosController : Controller
    {
        private readonly ContextoBD _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductosController(ContextoBD context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Productos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Productos.ToListAsync());
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos
                .FirstOrDefaultAsync(m => m.Producto_ID == id);
            if (productos == null)
            {
                return NotFound();
            }

            return View(productos);
        }

        // GET: Productos/Create
        [Authorize(Roles = "Supervisor,Administrador,Empleado")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Producto_ID,Producto_Nombre,Marca,Division,Categoria,Color,Talla,Stock,Precio_Compra,Precio_Venta")] Productos productos)
        {

            ValidationContext val = new ValidationContext(productos, null, null);
            List<ValidationResult> errores = new List<ValidationResult>();
            Validator.TryValidateObject(productos, val, errores);


            if (errores.Count == 0)
            {
                _context.Add(productos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productos);
        }

        [Authorize(Roles ="Supervisor,Administrador")]
        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos.FindAsync(id);
            if (productos == null)
            {
                return NotFound();
            }
            return View(productos);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Producto_ID,Producto_Nombre,Marca,Division,Categoria,Color,Talla,Stock,Precio_Compra,Precio_Venta,Total")] Productos productos)
        {

            if (id != productos.Producto_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    productos.Precio_Compra = productos.Total / productos.Stock;
                    _context.Update(productos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductosExists(productos.Producto_ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productos);
        }

        // GET: Productos/Delete/5
        [Authorize(Roles = "Supervisor,Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos
                .FirstOrDefaultAsync(m => m.Producto_ID == id);
            if (productos == null)
            {
                return NotFound();
            }

            return View(productos);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productos = await _context.Productos.FindAsync(id);
            _context.Productos.Remove(productos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductosExists(int id)
        {
            return _context.Productos.Any(e => e.Producto_ID == id);
        }
    }
}
