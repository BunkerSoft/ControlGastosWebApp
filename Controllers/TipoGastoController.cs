// Controllers/TipoGastoController.cs
using ControlGastosWebApp.Data;
using ControlGastosWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControlGastosWebApp.Controllers
{
    public class TipoGastoController : Controller
    {
        private readonly AppDbContext _context;

        public TipoGastoController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var tipos = await _context.TiposGasto.ToListAsync();
            return View(tipos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TipoGasto tipo)
        {
            if (ModelState.IsValid)
            {
                _context.TiposGasto.Add(tipo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipo);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var tipo = await _context.TiposGasto.FindAsync(id);
            if (tipo == null) return NotFound();
            return View(tipo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TipoGasto tipo)
        {
            if (ModelState.IsValid)
            {
                _context.TiposGasto.Update(tipo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipo);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var tipo = await _context.TiposGasto.FindAsync(id);
            if (tipo == null) return NotFound();
            _context.TiposGasto.Remove(tipo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
