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
    public class DepositoController : Controller
    {
        private readonly AppDbContext _context;

        public DepositoController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var depositos = _context.Depositos
                .Include(d => d.FondoMonetario)
                .ToList();
            return View(depositos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.FondosMonetarios = new SelectList(_context.FondosMonetarios, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Deposito deposito)
        {
            if (ModelState.IsValid)
            {
                deposito.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Depositos.Add(deposito);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deposito);
        }
    }
}