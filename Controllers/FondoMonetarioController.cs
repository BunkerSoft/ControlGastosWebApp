using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlGastosWebApp.Data;
using ControlGastosWebApp.Models;
using System.Threading.Tasks;
using System.Linq;
using ControlGastosWebApp.Models;
using ControlGastosWebApp.Data;


namespace ControlGastosWebApp.Controllers
{
    public class FondoMonetarioController : Controller
    {
        private readonly AppDbContext _context;
        public FondoMonetarioController(AppDbContext context) { _context = context; }

        public async Task<IActionResult> Index()
        {
            return View(await _context.FondosMonetarios.ToListAsync());
        }

        public IActionResult Create()
        {
            // Inicializar modelo con valores válidos por defecto
            var model = new FondoMonetario
            {
                FechaCreacion = DateTime.Today,
                Monto = 0
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(FondoMonetario model)
        {
            if (string.IsNullOrWhiteSpace(model.Nombre))
                ModelState.AddModelError("Nombre", "El nombre es obligatorio.");
            if (string.IsNullOrWhiteSpace(model.Tipo))
                ModelState.AddModelError("Tipo", "El tipo es obligatorio.");
            if (string.IsNullOrWhiteSpace(model.Descripcion))
                ModelState.AddModelError("Descripcion", "La descripción es obligatoria.");
            if (model.Monto <= 0)
                ModelState.AddModelError("Monto", "El monto debe ser mayor a cero.");
            if (model.FechaCreacion == default)
                ModelState.AddModelError("FechaCreacion", "Debe ingresar la fecha de creación.");

            if (ModelState.IsValid)
            {
                _context.FondosMonetarios.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.FondosMonetarios.FindAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, FondoMonetario model)
        {
            if (id != model.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.FondosMonetarios.FindAsync(id);
            if (item == null) return NotFound();

            _context.FondosMonetarios.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
