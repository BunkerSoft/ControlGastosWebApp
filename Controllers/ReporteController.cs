// 3. Controllers/ReporteController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ControlGastosWebApp.Data;
using ControlGastosWebApp.Models;

namespace ControlGastosWebApp.Controllers
{
    [Authorize]
    public class ReporteController : Controller
    {
        private readonly AppDbContext _context;

        public ReporteController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}