using Microsoft.EntityFrameworkCore;
using reto_citikold_back.Models;
using System.Collections.Generic;

namespace reto_citikold_back.Data
{
  public class AppDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Factura> facturas { get; set; }
        public DbSet<DetalleFactura> detalleFacturas { get; set; }

        public DbSet<UserCitikold> User { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasData(
                   new Client(){
                       idCliente = 1,
                       nombre = "Daniel",
                       correo = "correo@tes.com",
                       direccion = "direccion",
                       rucDni = "002318",
                       activo = true,
                       fechaCreacion = new DateTime()

                   }
                );

            modelBuilder.Entity<UserCitikold>().HasData(
                new UserCitikold()
                {
                    idUser=1,
                    username = "Daniel",
                    password = "password",
                    activo= true,
                    intentosIncorrecto=0

                }
             );
            modelBuilder.Entity<Factura>()
                       .HasOne(f => f.Cliente)
                       .WithMany(c => c.facturas)
                       .HasForeignKey(f => f.IdClient)
                       .IsRequired();
        }
    }
}
