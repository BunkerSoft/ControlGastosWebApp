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

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(FondoMonetario model)
        {
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
