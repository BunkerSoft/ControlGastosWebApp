using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ControlGastosWebApp.Models
{
    public class TipoGasto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        
        public string Descripcion { get; set; }
        
        public ICollection<Presupuesto> Presupuestos { get; set; }
    }
}
