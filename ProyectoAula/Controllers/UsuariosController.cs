using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoAula.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ProyectoAula.Controllers
{
    [Authorize(Roles ="Supervisor")]
    public class UsuariosController : Controller
    {
        private readonly ContextoBD _context;

        public UsuariosController(ContextoBD context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Usuario_ID == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create() => View();

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public IActionResult Registrar([Bind("Usuario_ID, Nombre, Correo, Clave, Roles")] Usuario usuario)
        {
            usuario.Clave = Utilerias.Encriptar.EncriptarPassword(usuario.Clave);

            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                _context.SaveChanges();
                var mensaje = String.Format("<h1>Hola {0}</h1><p>Se completo correctamente tu registro</p>", usuario.Nombre);

                try
                {
                    Utilerias.CorreoElectronico.Enviar(usuario.Correo, "ProyectoAula::Registro completado", mensaje);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index", new { completo = true, correo = false });
                }
                return RedirectToAction("Index", new { completo = true });
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Usuario_ID,Nombre,Correo,Clave,Roles")] Usuario usuario)
        {
            if (id != usuario.Usuario_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    usuario.Clave = Utilerias.Encriptar.EncriptarPassword(usuario.Clave);
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Usuario_ID))
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
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Usuario_ID == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Usuario_ID == id);
        }
    }
}
