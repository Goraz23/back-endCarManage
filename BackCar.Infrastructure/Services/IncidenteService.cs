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
    public class IncidenteService : IIncidenteService
    {
        private readonly ApplicationDbContext _context;

        public IncidenteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Incidente>> ObtenerTodosAsync()
        {
            try
            {
                var incidentes = await _context.Incidentes.Include(i => i.ContratoRenta).ToListAsync();
                Log.Information("Se obtuvieron todos los incidentes correctamente.");
                return incidentes;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener todos los incidentes.");
                throw;
            }
        }

        public async Task<Incidente> ObtenerPorIdAsync(int id)
        {
            try
            {
                var incidente = await _context.Incidentes.Include(i => i.ContratoRenta)
                                                          .FirstOrDefaultAsync(i => i.Id_Incidente == id);
                if (incidente == null)
                {
                    Log.Warning("Incidente con ID {Id} no encontrado.", id);
                }
                else
                {
                    Log.Information("Incidente con ID {Id} obtenido correctamente.", id);
                }

                return incidente;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener el incidente con ID {Id}.", id);
                throw;
            }
        }

        public async Task<Incidente> CrearAsync(Incidente incidente)
        {
            try
            {
                _context.Incidentes.Add(incidente);
                await _context.SaveChangesAsync();
                Log.Information("Incidente creado correctamente.");
                return incidente;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al crear un nuevo incidente.");
                throw;
            }
        }

        public async Task<Incidente> ActualizarAsync(int id, Incidente incidente)
        {
            try
            {
                var existente = await _context.Incidentes.FindAsync(id);
                if (existente == null)
                {
                    Log.Warning("Incidente con ID {Id} no encontrado para actualización.", id);
                    return null;
                }

                existente.FechaIncidente = incidente.FechaIncidente;
                existente.Descripcion = incidente.Descripcion;
                existente.AplicoSeguro = incidente.AplicoSeguro;
                existente.Id_ContratoRenta = incidente.Id_ContratoRenta;

                await _context.SaveChangesAsync();
                Log.Information("Incidente con ID {Id} actualizado correctamente.", id);
                return existente;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al actualizar el incidente con ID {Id}.", id);
                throw;
            }
        }

        public async Task<bool> EliminarAsync(int id)
        {
            try
            {
                var incidente = await _context.Incidentes.FindAsync(id);
                if (incidente == null)
                {
                    Log.Warning("Incidente con ID {Id} no encontrado para eliminación.", id);
                    return false;
                }

                _context.Incidentes.Remove(incidente);
                await _context.SaveChangesAsync();
                Log.Information("Incidente con ID {Id} eliminado correctamente.", id);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al eliminar el incidente con ID {Id}.", id);
                throw;
            }
        }
    }
}
