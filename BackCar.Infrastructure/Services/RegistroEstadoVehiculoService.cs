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
    public class RegistroEstadoVehiculoService : IRegistroEstadoVehiculoService
    {
        private readonly ApplicationDbContext _context;

        public RegistroEstadoVehiculoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<RegistroEstadoVehiculo>> ObtenerTodosAsync()
        {
            try
            {
                Log.Information("Obteniendo todos los registros de estado de vehículos.");
                return await _context.RegistroEstadoVehiculos.Include(r => r.ContratoRenta).ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener todos los registros de estado de vehículos.");
                throw;
            }
        }

        public async Task<RegistroEstadoVehiculo> CrearAsync(RegistroEstadoVehiculo registro)
        {
            try
            {
                Log.Information("Creando un nuevo registro de estado de vehículo.");
                await _context.RegistroEstadoVehiculos.AddAsync(registro);
                await _context.SaveChangesAsync();
                Log.Information("Registro creado con éxito. ID: {Id}", registro.Id_RegistroEstadoVehiculo);
                return registro;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al crear un registro de estado de vehículo.");
                throw;
            }
        }

        public async Task<RegistroEstadoVehiculo> ActualizarAsync(int id, RegistroEstadoVehiculo registro)
        {
            try
            {
                Log.Information("Actualizando el registro de estado de vehículo con ID: {Id}", id);
                var registroExistente = await _context.RegistroEstadoVehiculos.FindAsync(id);

                if (registroExistente == null)
                {
                    Log.Warning("El registro de estado de vehículo con ID: {Id} no fue encontrado.", id);
                    return null;
                }

                registroExistente.KilometrajeInicial = registro.KilometrajeInicial;
                registroExistente.KilometrajeFinal = registro.KilometrajeFinal;
                registroExistente.DetallesRetorno = registro.DetallesRetorno;
                registroExistente.AplicanCargos = registro.AplicanCargos;
                registroExistente.Id_ContratoRenta = registro.Id_ContratoRenta;

                await _context.SaveChangesAsync();
                Log.Information("Registro actualizado con éxito. ID: {Id}", id);
                return registroExistente;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al actualizar el registro de estado de vehículo con ID: {Id}", id);
                throw;
            }
        }

        public async Task<bool> EliminarAsync(int id)
        {
            try
            {
                Log.Information("Eliminando el registro de estado de vehículo con ID: {Id}", id);
                var registro = await _context.RegistroEstadoVehiculos.FindAsync(id);

                if (registro == null)
                {
                    Log.Warning("El registro de estado de vehículo con ID: {Id} no fue encontrado.", id);
                    return false;
                }

                _context.RegistroEstadoVehiculos.Remove(registro);
                await _context.SaveChangesAsync();
                Log.Information("Registro eliminado con éxito. ID: {Id}", id);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al eliminar el registro de estado de vehículo con ID: {Id}", id);
                throw;
            }
        }
    }
}
