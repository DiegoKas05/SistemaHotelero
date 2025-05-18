using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaHotelero.Models;

namespace SistemaHotelero.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        // aqui iran todos los modelos
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Piso> Piso { get; set; }
        public DbSet<EstadoHabitacion> EstadoHabitacion { get; set; }
        public DbSet<Habitacion> Habitacion { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Recepcion> Recepcion { get; set; }
        public DbSet<Venta> Venta { get; set; }
        public DbSet<DetalleVenta> DetalleVenta { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Habitacion>()
                .HasOne(h => h.EstadoHabitacion)
                .WithMany()
                .HasForeignKey(h => h.IdEstadoHabitacion)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull); 

            builder.Entity<Habitacion>()
                .HasOne(h => h.Piso)
                .WithMany()
                .HasForeignKey(h => h.IdPiso)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Habitacion>()
                .HasOne(h => h.Categoria)
                .WithMany()
                .HasForeignKey(h => h.IdCategoria)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}