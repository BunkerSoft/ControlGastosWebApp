// 8. Models/Presupuesto.cs (si no existe)
using System.ComponentModel.DataAnnotations;

namespace ControlGastosWebApp.Models
{
    public class Presupuesto
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;
        
        [Required]
        public decimal Monto { get; set; }
        
        [Required]
        public DateTime FechaInicio { get; set; }
        
        [Required]
        public DateTime FechaFin { get; set; }
        
        public int CategoriaId { get; set; }
        public virtual Categoria? Categoria { get; set; }
        
        public string? UsuarioId { get; set; }
        public virtual ApplicationUser? Usuario { get; set; }

        public int Mes { get; set; }
        public int Año { get; set; }
        public int TipoGastoId { get; set; }
        public TipoGasto? TipoGasto { get; set; }
    }
}