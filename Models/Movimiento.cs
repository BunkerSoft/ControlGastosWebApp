// 6. Models/Movimiento.cs (si no existe)
using System.ComponentModel.DataAnnotations;

namespace ControlGastosWebApp.Models
{
    public class Movimiento
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Descripcion { get; set; } = string.Empty;
        
        [Required]
        public decimal Monto { get; set; }
        
        [Required]
        public DateTime Fecha { get; set; } = DateTime.Now;
        
        public int CategoriaId { get; set; }
        public virtual Categoria? Categoria { get; set; }
        
        public string? UsuarioId { get; set; }
        public virtual ApplicationUser? Usuario { get; set; }
        
        public TipoMovimiento Tipo { get; set; }
        
        public int FondoMonetarioId { get; set; }
        public FondoMonetario? FondoMonetario { get; set; }
        public int TipoGastoId { get; set; }
        public TipoGasto? TipoGasto { get; set; }
        public string NombreComercio { get; set; } = string.Empty;
        public decimal MontoTotal { get; set; }
    }
    
    public enum TipoMovimiento
    {
        Ingreso = 1,
        Gasto = 2
    }
}