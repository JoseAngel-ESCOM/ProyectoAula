using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoAula.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoAula.Controllers
{
    [Authorize]
    public class ProveedoresController : Controller
    {
        private readonly ContextoBD _context;

        public ProveedoresController(ContextoBD context)
        {
            _context = context;
        }

        // GET: Proveedores
        [Authorize(Roles = "Supervisor,Administrador,Empleado")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Proveedores.ToListAsync());
        }

        // GET: Proveedores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedores = await _context.Proveedores
                .FirstOrDefaultAsync(m => m.Proveedores_ID == id);
            if (proveedores == null)
            {
                return NotFound();
            }

            return View(proveedores);
        }

        // GET: Proveedores/Create
        [Authorize(Roles = "Supervisor,Administrador,Empleado")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Proveedores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Proveedores_ID,RazonSocial,Telefono,Correo")] Proveedores proveedores)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proveedores);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proveedores);
        }

        // GET: Proveedores/Edit/5
        [Authorize(Roles = "Supervisor,Administrador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedores = await _context.Proveedores.FindAsync(id);
            if (proveedores == null)
            {
                return NotFound();
            }
            return View(proveedores);
        }

        // POST: Proveedores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Proveedores_ID,RazonSocial,Telefono,Correo")] Proveedores proveedores)
        {
            if (id != proveedores.Proveedores_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proveedores);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProveedoresExists(proveedores.Proveedores_ID))
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
            return View(proveedores);
        }

        // GET: Proveedores/Delete/5
        [Authorize(Roles = "Supervisor,Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedores = await _context.Proveedores
                .FirstOrDefaultAsync(m => m.Proveedores_ID == id);
            if (proveedores == null)
            {
                return NotFound();
            }

            return View(proveedores);
        }

        // POST: Proveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proveedores = await _context.Proveedores.FindAsync(id);
            _context.Proveedores.Remove(proveedores);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProveedoresExists(int id)
        {
            return _context.Proveedores.Any(e => e.Proveedores_ID == id);
        }
    }
}
