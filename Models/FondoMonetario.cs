using System.ComponentModel.DataAnnotations;

namespace ControlGastosWebApp.Models
{
    public class FondoMonetario
    {
        public int Id { get; set; }

        [Required]
        public decimal Monto { get; set; }

        [Required]
        [StringLength(100)]
        public string Descripcion { get; set; } = string.Empty;

        public DateTime FechaCreacion { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Tipo { get; set; } = string.Empty;
    }
}