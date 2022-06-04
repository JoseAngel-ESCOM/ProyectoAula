using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using System;
using System.Threading.Tasks;
using ProyectoAula.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProyectoAula.Controllers
{
    public class HomeController : Controller
    {

        private readonly ContextoBD _context;

        public HomeController(ContextoBD context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}