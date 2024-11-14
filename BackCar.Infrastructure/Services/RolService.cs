using BackCar._Domain.Entities;
using BackCar.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackCar.Infrastructure.Services
{
    public class RolService : IRolService
    {
        private readonly ApplicationDbContext _context;

        public RolService(ApplicationDbContext context)
        {
            _context = context;
        }

     
        public async Task<List<Rol>> ObtenerTodosLosRolesAsync()
        {
            Console.WriteLine("Conectando a la base de datos...");  // Mensaje de depuración
            var roles = await _context.Roles.ToListAsync();
            Console.WriteLine($"Número de roles obtenidos: {roles.Count}");  // Verifica la cantidad de roles obtenidos
            return roles;
        }

        public async Task CrearRolAsync(Rol nuevoRol)  // Implementación del método para crear
        {
            await _context.Roles.AddAsync(nuevoRol);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EliminarRolAsync(int id)
        {
            var rol = await _context.Roles.FindAsync(id);
            if (rol == null)
                return false;

            _context.Roles.Remove(rol);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActualizarRolAsync(int id, Rol rolActualizado)
        {
            var rolExistente = await _context.Roles.FindAsync(id);
            if (rolExistente == null)
                return false;

            // Actualiza las propiedades del rol
            rolExistente.Nombre = rolActualizado.Nombre;

            _context.Roles.Update(rolExistente);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
