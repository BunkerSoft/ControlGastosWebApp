using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ControlGastosWebApp.Models;
using ControlGastosWebApp.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace ControlGastosWebApp.Controllers
{
    [Authorize]
    public class GastoController : Controller
    {
        private readonly AppDbContext _context;

        public GastoController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var gastos = _context.Movimientos
                //.Where(m => m.TipoMovimiento == TipoMovimiento.Gasto)
                //.Include(m => m.Detalles)
                .Include(m => m.FondoMonetario)
                .ToList();
            return View(gastos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.FondosMonetarios = new SelectList(_context.FondosMonetarios, "Id", "Nombre");
            ViewBag.TiposGasto = new SelectList(_context.TiposGasto, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Movimiento movimiento)
        {
            if (ModelState.IsValid)
            {
                //movimiento.TipoMovimiento = TipoMovimiento.Gasto;
                //movimiento.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                // Eliminar validación de presupuesto y detalles porque no existen en el modelo
                _context.Movimientos.Add(movimiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movimiento);
        }
    }
}