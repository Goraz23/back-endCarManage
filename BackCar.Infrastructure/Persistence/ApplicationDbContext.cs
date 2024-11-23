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
<<<<<<< HEAD

=======
>>>>>>> 08004e7f1f59ce83fc862f281e380c15474ca09c
        public DbSet<Cliente> Clientes { get; set; } // Nuevo DbSet para Cliente
        public DbSet<Seguro> Seguros { get; set; } // Nuevo DbSet para Seguro
        public DbSet<RegistroEstadoVehiculo> RegistroEstadoVehiculos { get; set; }
        public DbSet<Incidente> Incidentes { get; set; }
<<<<<<< HEAD
=======

        public DbSet<VehiculoSeguro> VehiculosSeguros { get; set; }
>>>>>>> 08004e7f1f59ce83fc862f281e380c15474ca09c

    }
}
