// 5. Models/ApplicationUser.cs (si no existe)
using Microsoft.AspNetCore.Identity;

namespace ControlGastosWebApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? NombreCompleto { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}