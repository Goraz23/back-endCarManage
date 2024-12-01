using BackCar._Domain.Entities;
using BackCar.Application.DTOs;
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
    public class ContratoRentaService : IContratoRentaService
    {
        private readonly ApplicationDbContext _context;

        public ContratoRentaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ContratoRentaDTO>> ObtenerTodosLosContratosAsync()
        {
            try
            {
                var contratos = await _context.ContratosRenta
                                              .Include(c => c.Cliente)
                                              .Include(c => c.Vehiculo)
                                              .ToListAsync();

                Log.Information("Contratos obtenidos correctamente.");
                return contratos.Select(c => new ContratoRentaDTO
                {
                    Id_ContratoRenta = c.Id_ContratoRenta,
                    FechaInicio = c.FechaInicio,
                    FechaDevolucion = c.FechaDevolucion,
                    CostoSubTotal = c.CostoSubTotal,
                    CostoTotal = c.CostoTotal,
                    FechaCreacion = c.FechaCreacion,
                    Clientes_id = c.Clientes_id,
                    Vehiculos_id = c.Vehiculos_id
                }).ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener los contratos.");
                throw;
            }
        }

        public async Task<ContratoRentaDTO> ObtenerContratoPorIdAsync(int id)
        {
            try
            {
                var contrato = await _context.ContratosRenta
                                             .Include(c => c.Cliente)
                                             .Include(c => c.Vehiculo)
                                             .FirstOrDefaultAsync(c => c.Id_ContratoRenta == id);

                if (contrato == null)
                {
                    Log.Warning("Contrato con ID {Id} no encontrado.", id);
                    return null;
                }

                Log.Information("Contrato con ID {Id} obtenido correctamente.", id);
                return new ContratoRentaDTO
                {
                    Id_ContratoRenta = contrato.Id_ContratoRenta,
                    FechaInicio = contrato.FechaInicio,
                    FechaDevolucion = contrato.FechaDevolucion,
                    CostoSubTotal = contrato.CostoSubTotal,
                    CostoTotal = contrato.CostoTotal,
                    FechaCreacion = contrato.FechaCreacion,
                    Clientes_id = contrato.Clientes_id,
                    Vehiculos_id = contrato.Vehiculos_id
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener el contrato con ID {Id}.", id);
                throw;
            }
        }

        public async Task CrearContratoAsync(ContratoRentaDTO nuevoContrato)
        {
            try
            {
                var contrato = new ContratoRenta
                {
                    FechaInicio = nuevoContrato.FechaInicio,
                    FechaDevolucion = nuevoContrato.FechaDevolucion,
                    CostoSubTotal = nuevoContrato.CostoSubTotal,
                    CostoTotal = nuevoContrato.CostoTotal,
                    FechaCreacion = nuevoContrato.FechaCreacion,
                    Clientes_id = nuevoContrato.Clientes_id,
                    Vehiculos_id = nuevoContrato.Vehiculos_id
                };

                await _context.ContratosRenta.AddAsync(contrato);
                await _context.SaveChangesAsync();
                Log.Information("Contrato creado correctamente.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al crear un nuevo contrato.");
                throw;
            }
        }

        public async Task<bool> EliminarContratoAsync(int id)
        {
            try
            {
                var contrato = await _context.ContratosRenta.FindAsync(id);
                if (contrato == null)
                {
                    Log.Warning("Contrato con ID {Id} no encontrado para eliminación.", id);
                    return false;
                }

                _context.ContratosRenta.Remove(contrato);
                await _context.SaveChangesAsync();
                Log.Information("Contrato con ID {Id} eliminado correctamente.", id);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al eliminar el contrato con ID {Id}.", id);
                throw;
            }
        }

        public async Task<bool> ActualizarContratoAsync(int id, ContratoRentaDTO contratoActualizado)
        {
            try
            {
                var contratoExistente = await _context.ContratosRenta.FindAsync(id);
                if (contratoExistente == null)
                {
                    Log.Warning("Contrato con ID {Id} no encontrado para actualización.", id);
                    return false;
                }

                contratoExistente.FechaInicio = contratoActualizado.FechaInicio;
                contratoExistente.FechaDevolucion = contratoActualizado.FechaDevolucion;
                contratoExistente.CostoSubTotal = contratoActualizado.CostoSubTotal;
                contratoExistente.CostoTotal = contratoActualizado.CostoTotal;
                contratoExistente.FechaCreacion = contratoActualizado.FechaCreacion;
                contratoExistente.Clientes_id = contratoActualizado.Clientes_id;
                contratoExistente.Vehiculos_id = contratoActualizado.Vehiculos_id;

                _context.ContratosRenta.Update(contratoExistente);
                await _context.SaveChangesAsync();
                Log.Information("Contrato con ID {Id} actualizado correctamente.", id);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al actualizar el contrato con ID {Id}.", id);
                throw;
            }
        }
    }
}
