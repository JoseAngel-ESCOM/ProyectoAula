using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoAula.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProyectoAula.Controllers
{
    public class AccesoController : Controller
    {

        private readonly ContextoBD _context;

        public AccesoController(ContextoBD context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string Correo, string Clave)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == Correo);

            if (usuario == null)
            {
                return RedirectToAction("Index", new { error = true });
            }

            if (usuario.Clave != Utilerias.Encriptar.EncriptarPassword(Clave))
            {
                return RedirectToAction("Index", new { error = true });
            }

            if (usuario != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Nombre),
                    new Claim(ClaimTypes.Role, usuario.Roles),
                    new Claim("Correo", usuario.Correo),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Registrar() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
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

        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return View("Index");
        }
    }
}
