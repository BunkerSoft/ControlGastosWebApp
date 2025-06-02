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
            // Mostrar todos los presupuestos si no se filtra por mes y año
            var presupuestos = _context.Presupuestos.Include(p => p.TipoGasto).AsQueryable();
            if (mes.HasValue && año.HasValue)
            {
                presupuestos = presupuestos.Where(p => p.Mes == mes.Value && p.Año == año.Value);
            }
            var lista = await presupuestos.ToListAsync();
            ViewBag.Mes = mes;
            ViewBag.Año = año;
            return View(lista);
        }

        public IActionResult Create()
        {
            ViewBag.TiposGasto = _context.TiposGasto.ToList();
            ViewBag.Categorias = _context.Categorias.ToList();
            // Inicializar modelo con valores mínimos y fechas válidas
            var model = new Presupuesto
            {
                Mes = DateTime.Now.Month,
                Año = DateTime.Now.Year,
                Monto = 0,
                FechaInicio = DateTime.Today,
                FechaFin = DateTime.Today.AddMonths(1)
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Presupuesto model, string MesInput)
        {
            if (!string.IsNullOrEmpty(MesInput))
            {
                var partes = MesInput.Split('-');
                if (partes.Length == 2 && int.TryParse(partes[0], out int año) && int.TryParse(partes[1], out int mes))
                {
                    model.Año = año;
                    model.Mes = mes;
                }
            }

            if (model.TipoGastoId == 0)
                ModelState.AddModelError("TipoGastoId", "Debe seleccionar un tipo de gasto.");
            if (model.CategoriaId == 0)
                ModelState.AddModelError("CategoriaId", "Debe seleccionar una categoría.");
            if (model.Monto <= 0)
                ModelState.AddModelError("Monto", "El monto debe ser mayor a cero.");
            if (string.IsNullOrWhiteSpace(model.Nombre))
                ModelState.AddModelError("Nombre", "El nombre es obligatorio.");
            if (model.FechaInicio == default)
                ModelState.AddModelError("FechaInicio", "Debe ingresar la fecha de inicio.");
            if (model.FechaFin == default)
                ModelState.AddModelError("FechaFin", "Debe ingresar la fecha de fin.");
            if (model.FechaFin < model.FechaInicio)
                ModelState.AddModelError("FechaFin", "La fecha de fin debe ser igual o posterior a la de inicio.");

            if (ModelState.IsValid)
            {
                var existe = await _context.Presupuestos.AnyAsync(p =>
                    p.Mes == model.Mes && p.Año == model.Año && p.TipoGastoId == model.TipoGastoId);
                if (existe)
                {
                    ModelState.AddModelError("", "Ya existe un presupuesto para ese mes, año y tipo de gasto.");
                    ViewBag.TiposGasto = _context.TiposGasto.ToList();
                    ViewBag.Categorias = _context.Categorias.ToList();
                    return View(model);
                }

                _context.Presupuestos.Add(model);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Presupuesto guardado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.TiposGasto = _context.TiposGasto.ToList();
            ViewBag.Categorias = _context.Categorias.ToList();
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.Presupuestos.FindAsync(id);
            if (item == null) return NotFound();

            ViewBag.TiposGasto = _context.TiposGasto.ToList();
            ViewBag.Categorias = _context.Categorias.ToList();
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Presupuesto model, string MesInput)
        {
            if (id != model.Id) return NotFound();

            // Parsear MesInput si viene del input type="month"
            if (!string.IsNullOrEmpty(MesInput))
            {
                var partes = MesInput.Split('-');
                if (partes.Length == 2 && int.TryParse(partes[0], out int año) && int.TryParse(partes[1], out int mes))
                {
                    model.Año = año;
                    model.Mes = mes;
                }
            }

            if (ModelState.IsValid)
            {
                _context.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.TiposGasto = _context.TiposGasto.ToList();
            ViewBag.Categorias = _context.Categorias.ToList();
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
