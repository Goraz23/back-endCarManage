using Microsoft.EntityFrameworkCore;
using BackCar._Domain.Entities; // Asegúrate de importar las entidades correctas

namespace BackCar.Infrastructure // Usa el espacio de nombres adecuado
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Rol> Roles { get; set; }  // Aquí defines tu DbSet para la tabla Roles_Usuario
        // Otros DbSets para tus entidades
    }
}
