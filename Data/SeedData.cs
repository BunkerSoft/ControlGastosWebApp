// 9. Data/SeedData.cs (si no existe)
using Microsoft.AspNetCore.Identity;
using ControlGastosWebApp.Models;

namespace ControlGastosWebApp.Data
{
    public static class SeedData
    {
        public static async Task Initialize(UserManager<ApplicationUser> userManager)
        {
            // Crear usuario administrador por defecto o actualizar contraseña si ya existe
            var adminEmail = "adminazure@controlgastos.com";
            var adminPassword = "Ok123!";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    NombreCompleto = "Administrador Azure",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(adminUser, adminPassword);
            }
            else
            {
                // Restablecer contraseña si el usuario ya existe
                await userManager.RemovePasswordAsync(adminUser);
                await userManager.AddPasswordAsync(adminUser, adminPassword);
            }
        }
    }
}