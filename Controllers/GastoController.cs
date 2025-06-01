using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ControlGastosWebApp.Models;
using ControlGastosWebApp.Data;
using ControlGastosWebApp.Models;
using ControlGastosWebApp.Data;

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
                .Where(m => m.TipoMovimiento == TipoMovimiento.Gasto)
                .Include(m => m.Detalles)
                .Include(m => m.FondoMonetario)
                .ToList();
            return View(gastos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.FondosMonetarios = new SelectList(_context.FondosMonetarios, "Id", "Nombre");
            ViewBag.TiposGasto = new SelectList(_context.TipoGastos, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Movimiento movimiento)
        {
            if (ModelState.IsValid)
            {
                movimiento.TipoMovimiento = TipoMovimiento.Gasto;
                movimiento.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                
                // Validar presupuesto
                foreach (var detalle in movimiento.Detalles)
                {
                    var presupuesto = await ValidarPresupuesto(detalle);
                    if (presupuesto.Sobregirado)
                    {
                        TempData["Warning"] = $"Presupuesto sobregirado en {presupuesto.TipoGasto}: {presupuesto.MontoSobregiro}";
                    }
                }

                _context.Movimientos.Add(movimiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movimiento);
        }
    }
}