// Ruta: Models/Deposito.cs
namespace ControlGastosWebApp.Models
{
    public class Deposito
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
        public string CuentaDestino { get; set; }
    }
}
