// 9. Data/SeedData.cs (si no existe)
using Microsoft.AspNetCore.Identity;
using ControlGastosWebApp.Models;

namespace ControlGastosWebApp.Data
{
    public static class SeedData
    {
        public static async Task Initialize(UserManager<ApplicationUser> userManager)
        {
            // Crear usuario administrador por defecto
            if (await userManager.FindByEmailAsync("admin@controlgastos.com") == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "admin@controlgastos.com",
                    Email = "admin@controlgastos.com",
                    NombreCompleto = "Administrador",
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(adminUser, "Admin123!");
            }
        }
    }
}