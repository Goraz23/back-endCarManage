using BackCar._Domain.Entities;
using BackCar.Application.DTOs;
using BackCar.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<IncidenteMapeoDto>> ObtenerTodosLosIncidentesAsync()
        {
            try
            {
                var incidentes = await _context.Incidentes
                                               .Include(i => i.ContratoRenta)
                                               .Select(i => new IncidenteMapeoDto
                                               {
                                                   Id_Incidente = i.Id_Incidente,
                                                   FechaIncidente = i.FechaIncidente,
                                                   Descripcion = i.Descripcion,
                                                   AplicoSeguro = i.AplicoSeguro,
                                                   Id_ContratoRenta = i.Id_ContratoRenta,
                                                   Id_Vehiculo = i.Id_Vehiculo
                                               })
                                               .ToListAsync();
                Log.Information("Se obtuvieron todos los incidentes correctamente.");
                return incidentes;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener todos los incidentes.");
                throw;
            }
        }

        public async Task<IncidenteDto> ObtenerIncidentePorIdAsync(int id)
        {
            try
            {
                var incidente = await _context.Incidentes
                                              .Include(i => i.ContratoRenta)
                                              .FirstOrDefaultAsync(i => i.Id_Incidente == id);

                if (incidente == null)
                {
                    Log.Warning("Incidente con ID {Id} no encontrado.", id);
                    return null;
                }

                Log.Information("Incidente con ID {Id} obtenido correctamente.", id);

                return new IncidenteDto
                {
                    Id_Incidente = incidente.Id_Incidente,
                    FechaIncidente = incidente.FechaIncidente,
                    Descripcion = incidente.Descripcion,
                    AplicoSeguro = incidente.AplicoSeguro,
                    Id_ContratoRenta = incidente.Id_ContratoRenta,
                    Id_Vehiculo = incidente.Id_Vehiculo
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener el incidente con ID {Id}.", id);
                throw;
            }
        }

        public async Task CrearIncidenteAsync(IncidenteDto nuevoIncidente)
        {
            try
            {
                var incidente = new Incidente
                {
                    FechaIncidente = nuevoIncidente.FechaIncidente,
                    Descripcion = nuevoIncidente.Descripcion,
                    AplicoSeguro = nuevoIncidente.AplicoSeguro,
                    Id_ContratoRenta = nuevoIncidente.Id_ContratoRenta,
                    Id_Vehiculo = nuevoIncidente.Id_Vehiculo
                };

                _context.Incidentes.Add(incidente);
                await _context.SaveChangesAsync();
                Log.Information("Incidente creado correctamente.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al crear un nuevo incidente.");
                throw;
            }
        }

        public async Task<bool> ActualizarIncidenteAsync(int id, IncidenteDto incidenteActualizado)
        {
            try
            {
                var incidente = await _context.Incidentes.FindAsync(id);
                if (incidente == null)
                {
                    Log.Warning("Incidente con ID {Id} no encontrado para actualización.", id);
                    return false;
                }

                incidente.FechaIncidente = incidenteActualizado.FechaIncidente;
                incidente.Descripcion = incidenteActualizado.Descripcion;
                incidente.AplicoSeguro = incidenteActualizado.AplicoSeguro;
                incidente.Id_ContratoRenta = incidenteActualizado.Id_ContratoRenta;
                incidente.Id_Vehiculo = incidenteActualizado.Id_Vehiculo;

                await _context.SaveChangesAsync();
                Log.Information("Incidente con ID {Id} actualizado correctamente.", id);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al actualizar el incidente con ID {Id}.", id);
                throw;
            }
        }

        public async Task<bool> EliminarIncidenteAsync(int id)
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

        public async Task<List<IncidenteDto>> ObtenerIncidentesPorVehiculoIdAsync(int vehiculoId)
        {
            try
            {
                // Verificar si el vehículo existe
                var vehiculo = await _context.Vehiculos.FindAsync(vehiculoId);
                if (vehiculo == null)
                {
                    Log.Warning("Vehículo con ID {VehiculoId} no encontrado.", vehiculoId);
                    return null; // O puedes devolver una lista vacía: return new List<IncidenteDto>();
                }

                // Obtener los incidentes asociados al vehículo
                var incidentes = await _context.Incidentes
                                               .Where(i => i.Id_Vehiculo == vehiculoId)
                                               .Select(i => new IncidenteDto
                                               {
                                                   Id_Incidente = i.Id_Incidente,
                                                   FechaIncidente = i.FechaIncidente,
                                                   Descripcion = i.Descripcion,
                                                   AplicoSeguro = i.AplicoSeguro,
                                                   Id_ContratoRenta = i.Id_ContratoRenta,
                                                   Id_Vehiculo = i.Id_Vehiculo
                                               })
                                               .ToListAsync();

                Log.Information("Incidentes del vehículo con ID {VehiculoId} obtenidos correctamente.", vehiculoId);
                return incidentes;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener incidentes del vehículo con ID {VehiculoId}.", vehiculoId);
                throw;
            }
        }


    }
}
