// 7. Models/Categoria.cs (si no existe)
using System.ComponentModel.DataAnnotations;

namespace ControlGastosWebApp.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;
        
        public string? Descripcion { get; set; }
        
        public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
    }
}