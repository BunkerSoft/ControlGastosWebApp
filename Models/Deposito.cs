// Ruta: Models/Deposito.cs
namespace ControlGastosWebApp.Models
{
    public class Deposito
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string CuentaDestino { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public int FondoMonetarioId { get; set; }
        public FondoMonetario? FondoMonetario { get; set; }
    }
}
