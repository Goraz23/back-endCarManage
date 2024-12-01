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
    public class SeguroService : ISeguroService
    {
        private readonly ApplicationDbContext _context;

        public SeguroService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Método READ: Obtener todos los seguros
        public async Task<List<Seguro>> ObtenerTodosLosSegurosAsync()
        {
            try
            {
                Log.Information("Obteniendo todos los seguros desde la base de datos...");
                var seguros = await _context.Seguros.ToListAsync();
                Log.Information($"Se obtuvieron {seguros.Count} seguros.");
                return seguros;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener los seguros.");
                throw;
            }
        }

        // Método CREATE: Crear un nuevo seguro
        public async Task<Seguro> CrearSeguroAsync(Seguro seguro)
        {
            try
            {
                Log.Information("Creando un nuevo seguro...");
                await _context.Seguros.AddAsync(seguro);
                await _context.SaveChangesAsync();
                Log.Information($"Seguro {seguro.TipoSeguro} creado exitosamente.");
                return seguro;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al crear un seguro.");
                throw;
            }
        }

        // Método UPDATE: Actualizar un seguro existente
        public async Task<Seguro> UpdateSeguroAsync(int id, Seguro seguro)
        {
            try
            {
                Log.Information($"Actualizando seguro con ID {id}...");
                var seguroExistente = await _context.Seguros.FindAsync(id);

                if (seguroExistente == null)
                {
                    Log.Warning($"Seguro con ID {id} no encontrado.");
                    return null; // No se encontró el seguro
                }

                seguroExistente.TipoSeguro = seguro.TipoSeguro;
                seguroExistente.FechaInicio = seguro.FechaInicio;
                seguroExistente.FechaVencimiento = seguro.FechaVencimiento;
                seguroExistente.AutorContratado = seguro.AutorContratado;
                seguroExistente.Monto = seguro.Monto;

                await _context.SaveChangesAsync();
                Log.Information($"Seguro con ID {id} actualizado exitosamente.");
                return seguroExistente;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error al actualizar seguro con ID {id}.");
                throw;
            }
        }

        // Método DELETE: Eliminar un seguro
        public async Task<bool> DeleteSeguroAsync(int id)
        {
            try
            {
                Log.Information($"Eliminando seguro con ID {id}...");
                // Buscar el seguro
                var seguro = await _context.Seguros.FindAsync(id);
                if (seguro == null)
                {
                    Log.Warning($"Seguro con ID {id} no encontrado.");
                    return false;
                }

                // Buscar registros relacionados en VehiculosSeguros
                var vehiculosSeguros = await _context.VehiculosSeguros
                    .Where(s => s.Seguros_id == id)
                    .ToListAsync();

                if (vehiculosSeguros.Any())
                {
                    _context.VehiculosSeguros.RemoveRange(vehiculosSeguros);
                    Log.Information($"Se eliminaron {vehiculosSeguros.Count} registros asociados.");
                }

                _context.Seguros.Remove(seguro);
                await _context.SaveChangesAsync();
                Log.Information($"Seguro con ID {id} eliminado exitosamente.");
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error al eliminar seguro con ID {id}.");
                throw;
            }
        }
    }
}
