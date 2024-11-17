﻿// <auto-generated />
using System;
using GestionVehiculos.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GestionVehiculos.Migrations
{
    [DbContext(typeof(DbContextApplication))]
    partial class DbContextApplicationModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GestionVehiculos.Models.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Contacto")
                        .HasColumnType("int");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Documento")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoCliente")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("GestionVehiculos.Models.Mantenimiento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Costo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("Proveedor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("VehiculoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VehiculoId");

                    b.ToTable("Mantenimientos");
                });

            modelBuilder.Entity("GestionVehiculos.Models.Seguro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Cobertertura")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Costo")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaVencimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Proveedor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("VehiculoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VehiculoId");

                    b.ToTable("Seguros");
                });

            modelBuilder.Entity("GestionVehiculos.Models.Vehiculo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Anio")
                        .HasColumnType("int");

                    b.Property<string>("Foto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ID_Cliente")
                        .HasColumnType("int");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ID_Cliente");

                    b.ToTable("Vehiculos");
                });

            modelBuilder.Entity("GestionVehiculos.Models.Mantenimiento", b =>
                {
                    b.HasOne("GestionVehiculos.Models.Vehiculo", "Vehiculo")
                        .WithMany("Mantenimientos")
                        .HasForeignKey("VehiculoId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Vehiculo");
                });

            modelBuilder.Entity("GestionVehiculos.Models.Seguro", b =>
                {
                    b.HasOne("GestionVehiculos.Models.Vehiculo", "vehiculo")
                        .WithMany("Seguros")
                        .HasForeignKey("VehiculoId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("vehiculo");
                });

            modelBuilder.Entity("GestionVehiculos.Models.Vehiculo", b =>
                {
                    b.HasOne("GestionVehiculos.Models.Cliente", "Cliente")
                        .WithMany("Vehiculos")
                        .HasForeignKey("ID_Cliente")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("GestionVehiculos.Models.Cliente", b =>
                {
                    b.Navigation("Vehiculos");
                });

            modelBuilder.Entity("GestionVehiculos.Models.Vehiculo", b =>
                {
                    b.Navigation("Mantenimientos");

                    b.Navigation("Seguros");
                });
#pragma warning restore 612, 618
        }
    }
}
