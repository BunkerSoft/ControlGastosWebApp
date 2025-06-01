// 4. Data/AppDbContext.cs (si no existe)
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ControlGastosWebApp.Models;

namespace ControlGastosWebApp.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Movimiento> Movimientos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Presupuesto> Presupuestos { get; set; }

        public DbSet<TipoGasto> TiposGasto { get; set; } // O TiposGastos, dependiendo de cómo lo uses en tu código
        public DbSet<Deposito> Depositos { get; set; }
        public DbSet<FondoMonetario> FondosMonetarios { get; set; }

  


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configuraciones adicionales del modelo
            builder.Entity<Movimiento>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Descripcion).HasMaxLength(200);
                entity.Property(e => e.Monto).HasColumnType("decimal(18,2)");
            });

            builder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            builder.Entity<Presupuesto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Monto).HasColumnType("decimal(18,2)");
            });
        }
    }
}