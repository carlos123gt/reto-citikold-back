﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using reto_citikold_back.Data;

#nullable disable

namespace retocitikoldback.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("reto_citikold_back.Models.Client", b =>
                {
                    b.Property<int>("idCliente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idCliente"));

                    b.Property<bool>("activo")
                        .HasColumnType("bit");

                    b.Property<string>("correo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("fechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("rucDni")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("idCliente");

                    b.ToTable("Clients");

                    b.HasData(
                        new
                        {
                            idCliente = 1,
                            activo = true,
                            correo = "correo@tes.com",
                            direccion = "direccion",
                            fechaCreacion = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            nombre = "Daniel",
                            rucDni = "002318"
                        });
                });

            modelBuilder.Entity("reto_citikold_back.Models.DetalleFactura", b =>
                {
                    b.Property<int>("IdDetalle")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDetalle"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<string>("CodigoProducto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FacturaIdFactura")
                        .HasColumnType("int");

                    b.Property<int>("IdFactura")
                        .HasColumnType("int");

                    b.Property<string>("NombreProducto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdDetalle");

                    b.HasIndex("FacturaIdFactura");

                    b.ToTable("detalleFacturas");
                });

            modelBuilder.Entity("reto_citikold_back.Models.Factura", b =>
                {
                    b.Property<int>("IdFactura")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdFactura"));

                    b.Property<int>("IdClient")
                        .HasColumnType("int");

                    b.Property<double>("Igv")
                        .HasColumnType("float");

                    b.Property<double>("SubTotal")
                        .HasColumnType("float");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.HasKey("IdFactura");

                    b.HasIndex("IdClient");

                    b.ToTable("facturas");
                });

            modelBuilder.Entity("reto_citikold_back.Models.Product", b =>
                {
                    b.Property<int>("idProducto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idProducto"));

                    b.Property<bool>("activo")
                        .HasColumnType("bit");

                    b.Property<string>("codigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("fechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("precio")
                        .HasColumnType("float");

                    b.Property<int>("stock")
                        .HasColumnType("int");

                    b.HasKey("idProducto");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("reto_citikold_back.Models.UserCitikold", b =>
                {
                    b.Property<int>("idUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idUser"));

                    b.Property<bool>("activo")
                        .HasColumnType("bit");

                    b.Property<int>("intentosIncorrecto")
                        .HasColumnType("int");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idUser");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            idUser = 1,
                            activo = true,
                            intentosIncorrecto = 0,
                            password = "password",
                            username = "Daniel"
                        });
                });

            modelBuilder.Entity("reto_citikold_back.Models.DetalleFactura", b =>
                {
                    b.HasOne("reto_citikold_back.Models.Factura", null)
                        .WithMany("Detalles")
                        .HasForeignKey("FacturaIdFactura");
                });

            modelBuilder.Entity("reto_citikold_back.Models.Factura", b =>
                {
                    b.HasOne("reto_citikold_back.Models.Client", "Cliente")
                        .WithMany("facturas")
                        .HasForeignKey("IdClient")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("reto_citikold_back.Models.Client", b =>
                {
                    b.Navigation("facturas");
                });

            modelBuilder.Entity("reto_citikold_back.Models.Factura", b =>
                {
                    b.Navigation("Detalles");
                });
#pragma warning restore 612, 618
        }
    }
}
