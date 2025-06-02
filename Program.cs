using Microsoft.EntityFrameworkCore;
using ControlGastosWebApp.Data;
using Microsoft.AspNetCore.Identity;
using DevExpress.AspNetCore;
using ControlGastosWebApp.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ControlGastosWebApp.Models.ApplicationUser, IdentityRole>(options => {
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 4;
})
.AddEntityFrameworkStores<AppDbContext>();

// Agregar DevExpress
builder.Services.AddDevExpressControls();

// Agregar MVC
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IPresupuestoService, PresupuestoService>();
builder.Services.AddSession();

var app = builder.Build();

// Configurar pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Crear usuario admin
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ControlGastosWebApp.Models.ApplicationUser>>();
    await SeedData.Initialize(userManager);
}

app.Run();


