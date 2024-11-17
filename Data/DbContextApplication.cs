using GestionVehiculos.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionVehiculos.Data
{
    public class DbContextApplication : DbContext
    { 
        public DbContextApplication(DbContextOptions<DbContextApplication> options) : base(options) { 
        
        
        }

        //Tablas DbSet

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Mantenimiento> Mantenimientos { get; set; }
        public DbSet<Seguro> Seguros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relación entre Cliente y Vehiculo
            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Vehiculos)
                .WithOne(v => v.Cliente)
                .HasForeignKey(v => v.ID_Cliente)
                .OnDelete(DeleteBehavior.SetNull);

            // Relación entre Vehiculo y Mantenimiento
            modelBuilder.Entity<Vehiculo>()
                .HasMany(v => v.Mantenimientos)
                .WithOne(m => m.Vehiculo)
                .HasForeignKey(m => m.VehiculoId) // Asegúrate de que este campo exista en Mantenimiento
                .OnDelete(DeleteBehavior.SetNull);

            // Relación entre Vehiculo y Seguro
            modelBuilder.Entity<Vehiculo>()
                .HasMany(v => v.Seguros)
                .WithOne(s => s.vehiculo)
                .HasForeignKey(s => s.VehiculoId) // Asegúrate de que este campo exista en Seguro
                .OnDelete(DeleteBehavior.SetNull);
               

            // Configuración del campo Costo en Seguro
            modelBuilder.Entity<Seguro>()
                .Property(s => s.Costo)
                .HasPrecision(10, 2);
        }


    }
}
