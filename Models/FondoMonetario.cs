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
        public string Descripcion { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}