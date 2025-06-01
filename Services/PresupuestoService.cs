// 2. Services/PresupuestoService.cs
using Microsoft.EntityFrameworkCore;
using ControlGastosWebApp.Data;
using ControlGastosWebApp.Models;

namespace ControlGastosWebApp.Services
{
    public class PresupuestoService : IPresupuestoService
    {
        private readonly AppDbContext _context;

        public PresupuestoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Movimiento>> ObtenerMovimientosAsync()
        {
            return await _context.Movimientos.ToListAsync();
        }

        public async Task<Movimiento> ObtenerMovimientoPorIdAsync(int id)
        {
            return await _context.Movimientos.FindAsync(id);
        }

        public async Task CrearMovimientoAsync(Movimiento movimiento)
        {
            _context.Movimientos.Add(movimiento);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarMovimientoAsync(Movimiento movimiento)
        {
            _context.Entry(movimiento).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task EliminarMovimientoAsync(int id)
        {
            var movimiento = await _context.Movimientos.FindAsync(id);
            if (movimiento != null)
            {
                _context.Movimientos.Remove(movimiento);
                await _context.SaveChangesAsync();
            }
        }
    }
}