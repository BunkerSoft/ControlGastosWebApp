
// 1. Services/IPresupuestoService.cs
using ControlGastosWebApp.Models;

namespace ControlGastosWebApp.Services
{
    public interface IPresupuestoService
    {
        Task<List<Movimiento>> ObtenerMovimientosAsync();
        Task<Movimiento> ObtenerMovimientoPorIdAsync(int id);
        Task CrearMovimientoAsync(Movimiento movimiento);
        Task ActualizarMovimientoAsync(Movimiento movimiento);
        Task EliminarMovimientoAsync(int id);
    }
}