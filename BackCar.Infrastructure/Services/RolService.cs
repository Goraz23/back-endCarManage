using BackCar._Domain.Entities;
using BackCar.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;
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
            try
            {
                Log.Information("Conectando a la base de datos para obtener roles...");
                var roles = await _context.Roles.ToListAsync();
                Log.Information($"Número de roles obtenidos: {roles.Count}");
                return roles;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener los roles.");
                throw;
            }
        }

        public async Task CrearRolAsync(Rol nuevoRol)
        {
            try
            {
                Log.Information("Creando un nuevo rol...");
                await _context.Roles.AddAsync(nuevoRol);
                await _context.SaveChangesAsync();
                Log.Information($"Rol {nuevoRol.Nombre} creado exitosamente.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al crear un rol.");
                throw;
            }
        }

        public async Task<bool> EliminarRolAsync(int id)
        {
            try
            {
                Log.Information($"Eliminando rol con ID: {id}...");
                var rol = await _context.Roles.FindAsync(id);
                if (rol == null)
                {
                    Log.Warning($"Rol con ID {id} no encontrado.");
                    return false;
                }

                _context.Roles.Remove(rol);
                await _context.SaveChangesAsync();
                Log.Information($"Rol con ID {id} eliminado exitosamente.");
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error al eliminar rol con ID {id}.");
                throw;
            }
        }

        public async Task<bool> ActualizarRolAsync(int id, Rol rolActualizado)
        {
            try
            {
                Log.Information($"Actualizando rol con ID: {id}...");
                var rolExistente = await _context.Roles.FindAsync(id);
                if (rolExistente == null)
                {
                    Log.Warning($"Rol con ID {id} no encontrado.");
                    return false;
                }

                rolExistente.Nombre = rolActualizado.Nombre;
                _context.Roles.Update(rolExistente);
                await _context.SaveChangesAsync();
                Log.Information($"Rol con ID {id} actualizado exitosamente.");
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error al actualizar rol con ID {id}.");
                throw;
            }
        }
    }
}
