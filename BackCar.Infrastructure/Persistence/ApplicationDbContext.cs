using Microsoft.EntityFrameworkCore;
using BackCar._Domain.Entities; // Asegúrate de importar las entidades correctas

namespace BackCar.Infrastructure // Usa el espacio de nombres adecuado
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        // Aquí defines tu DbSet para la tabla Roles_Usuario
        // Otros DbSets para tus entidades
        public DbSet<Categoria> Categorias { get; set; } // Nuevo DbSet para Categoria
        public DbSet<Vehiculo> Vehiculos { get; set; } // Nuevo DbSet para Vehiculo
        public DbSet<Mantenimiento> Mantenimientos { get; set; } // Nuevo DbSet para mantenimiento
        public DbSet<ContratoRenta> ContratosRenta { get; set; } // Nuevo DbSet para Vehiculo

        public DbSet<Cliente> Clientes { get; set; } // Nuevo DbSet para Cliente
        public DbSet<Seguro> Seguros { get; set; } // Nuevo DbSet para Seguro
        public DbSet<RegistroEstadoVehiculo> RegistroEstadoVehiculos { get; set; }
        public DbSet<Incidente> Incidentes { get; set; }
        public DbSet<VehiculoSeguro> VehiculosSeguros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Roles
            modelBuilder.Entity<Rol>().HasData(
                new Rol { Id_Rol = 1, Nombre = "Administrador" },
                new Rol { Id_Rol = 2, Nombre = "Usuario" }
            );

            // 2. Usuarios (depende de Rol)
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id_Usuario = 1,
                    Nombre = "Admin Principal",
                    Correo = "admin@carmanage.com",
                    Contrasenia = "hashedpassword",  // Cambia esto por un hash real
                    Telefono = "5551234567",
                    FechaRegistro = DateTime.Now,
                    Roles_Usuarios_id = 1
                },
                new Usuario
                {
                    Id_Usuario = 2,
                    Nombre = "Usuario General",
                    Correo = "usuario@carmanage.com",
                    Contrasenia = "hashedpassword",
                    Telefono = "5559876543",
                    FechaRegistro = DateTime.Now,
                    Roles_Usuarios_id = 2
                }
            );

            // 3. Categorias
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { Id_Categoria = 1, Nombre = "SUV" },
                new Categoria { Id_Categoria = 2, Nombre = "Sedán" },
                new Categoria { Id_Categoria = 3, Nombre = "Camioneta" }
            );

            // 4. Clientes
            modelBuilder.Entity<Cliente>().HasData(
                new Cliente { Id_Cliente = 1, Correo = "cliente1@gmail.com", NombreCliente = "Juan Perez", Telefono = "1234567890" },
                new Cliente { Id_Cliente = 2, Correo = "cliente2@gmail.com", NombreCliente = "Diego Aleman", Telefono = "1234567890" }
            );

            // 5. Seguros
            modelBuilder.Entity<Seguro>().HasData(
                new Seguro
                {
                    Id_Seguro = 1,
                    TipoSeguro = "Seguro Básico",
                    Monto = 50.00m,
                    FechaInicio = new DateTime(2025, 6, 1),
                    FechaVencimiento = new DateTime(2025, 6, 13),
                    AutorContratado = "Autor 1"
                },
                new Seguro
                {
                    Id_Seguro = 2,
                    TipoSeguro = "Seguro Completo",
                    Monto = 100.00m,
                    FechaInicio = new DateTime(2025, 6, 1),
                    FechaVencimiento = new DateTime(2025, 6, 16),
                    AutorContratado = "Autor 2"
                }
            );

            // 6. Vehiculos (depende de Categoria y Usuario)
            modelBuilder.Entity<Vehiculo>().HasData(
                new Vehiculo
                {
                    Id_Vehiculo = 1,
                    Placa = "ABC123",
                    Marca = "Toyota",
                    Modelo = "RAV4",
                    Anio = 2020,
                    Categoria_Id = 1,
                    Usuarios_id = 1,
                    Descripcion = "SUV familiar",
                    Kilometraje = 10000,
                    FechaRegistro = new DateTime(2025, 1, 1),
                    CostoTemporadaAlta = 1500.00m,
                    CostoTemporadaBaja = 1000.00m,
                    IsRentado = false,
                    IsMantenimiento = false,
                    IsAutomatico = true
                },
                new Vehiculo
                {
                    Id_Vehiculo = 2,
                    Placa = "XYZ456",
                    Marca = "Honda",
                    Modelo = "Civic",
                    Anio = 2019,
                    Categoria_Id = 2,
                    Usuarios_id = 2,
                    Descripcion = "Sedán compacto",
                    Kilometraje = 20000,
                    FechaRegistro = new DateTime(2025, 2, 1),
                    CostoTemporadaAlta = 1200.00m,
                    CostoTemporadaBaja = 900.00m,
                    IsRentado = false,
                    IsMantenimiento = false,
                    IsAutomatico = false
                }
            );

            // 7. ContratosRenta (depende de Vehiculos y Clientes)
            modelBuilder.Entity<ContratoRenta>().HasData(
                new ContratoRenta
                {
                    Id_ContratoRenta = 1,
                    Vehiculos_id = 1,
                    Clientes_id = 1,
                    FechaInicio = new DateTime(2025, 6, 1),
                    FechaDevolucion = new DateTime(2025, 6, 8),
                    CostoTotal = 350.00m
                },
                new ContratoRenta
                {
                    Id_ContratoRenta = 2,
                    Vehiculos_id = 2,
                    Clientes_id = 2,
                    FechaInicio = new DateTime(2025, 6, 10),
                    FechaDevolucion = new DateTime(2025, 6, 17),
                    CostoTotal = 200.00m
                }
            );

            // 8. Mantenimientos (depende de Vehiculos)
            modelBuilder.Entity<Mantenimiento>().HasData(
                new Mantenimiento
                {
                    Id_Mantenimiento = 1,
                    Vehiculo_id = 1,
                    Fecha = new DateTime(2025, 5, 10),
                    Detalles = "Cambio de aceite",
                    Costo = 50.00m,
                    Tipo = "mecánico"
                },
                new Mantenimiento
                {
                    Id_Mantenimiento = 2,
                    Vehiculo_id = 2,
                    Fecha = new DateTime(2025, 5, 20),
                    Detalles = "Revisión de frenos",
                    Costo = 75.00m,
                    Tipo = "mecánico"
                }
            );

            // 9. RegistroEstadoVehiculos (depende de ContratosRenta)
            modelBuilder.Entity<RegistroEstadoVehiculo>().HasData(
                new RegistroEstadoVehiculo
                {
                    Id_RegistroEstadoVehiculo = 1,
                    KilometrajeInicial = 10000,
                    KilometrajeFinal = 10500,
                    DetallesRetorno = "Todo en orden",
                    AplicanCargos = false,
                    Id_ContratoRenta = 1
                },
                new RegistroEstadoVehiculo
                {
                    Id_RegistroEstadoVehiculo = 2,
                    KilometrajeInicial = 20000,
                    KilometrajeFinal = 19800,
                    DetallesRetorno = "Kilometraje menor al esperado",
                    AplicanCargos = true,
                    Id_ContratoRenta = 2
                }
            );

            // 10. Incidentes (depende de Vehiculos y ContratosRenta)
            modelBuilder.Entity<Incidente>().HasData(
                new Incidente
                {
                    Id_Incidente = 1,
                    Descripcion = "Rasguño en la puerta",
                    FechaIncidente = new DateTime(2025, 6, 1),
                    Id_Vehiculo = 1,
                    AplicoSeguro = false,
                    Id_ContratoRenta = 1
                },
                new Incidente
                {
                    Id_Incidente = 2,
                    Descripcion = "Vidrio roto y puertas rayadas",
                    FechaIncidente = new DateTime(2025, 6, 15),
                    Id_Vehiculo = 2,
                    AplicoSeguro = false,
                    Id_ContratoRenta = 2
                }
            );

            // 11. VehiculosSeguros (depende de Vehiculos y Seguros)
            modelBuilder.Entity<VehiculoSeguro>().HasData(
                new VehiculoSeguro { Id_VehiculoSeguro = 1, Vehiculos_id = 1, Seguros_id = 1 },
                new VehiculoSeguro { Id_VehiculoSeguro = 2, Vehiculos_id = 2, Seguros_id = 2 }
            );
        }
    }
}
