using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlGastosWebApp.Data;
using ControlGastosWebApp.Models;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace ControlGastosWebApp.Controllers
{
    public class PresupuestoController : Controller
    {
        private readonly AppDbContext _context;
        public PresupuestoController(AppDbContext context) { _context = context; }

        public async Task<IActionResult> Index(int? mes, int? año)
        {
            if (!mes.HasValue) mes = DateTime.Now.Month;
            if (!año.HasValue) año = DateTime.Now.Year;

            var presupuestos = await _context.Presupuestos
                .Include(p => p.TipoGasto)
                .Where(p => p.Mes == mes.Value && p.Año == año.Value)
                .ToListAsync();

            ViewBag.Mes = mes;
            ViewBag.Año = año;

            return View(presupuestos);
        }

        public IActionResult Create()
        {
            ViewBag.TiposGasto = _context.TiposGasto.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Presupuesto model)
        {
            if (ModelState.IsValid)
            {
                var existe = await _context.Presupuestos.AnyAsync(p =>
                    p.Mes == model.Mes && p.Año == model.Año && p.TipoGastoId == model.TipoGastoId);
                if (existe)
                {
                    ModelState.AddModelError("", "Ya existe un presupuesto para ese mes, año y tipo de gasto.");
                    ViewBag.TiposGasto = _context.TiposGasto.ToList();
                    return View(model);
                }

                _context.Presupuestos.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.TiposGasto = _context.TiposGasto.ToList();
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.Presupuestos.FindAsync(id);
            if (item == null) return NotFound();

            ViewBag.TiposGasto = _context.TiposGasto.ToList();
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Presupuesto model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.TiposGasto = _context.TiposGasto.ToList();
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Presupuestos.FindAsync(id);
            if (item == null) return NotFound();

            _context.Presupuestos.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
