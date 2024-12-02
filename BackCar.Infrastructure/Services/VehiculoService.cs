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
    public class VehiculoService : IVehiculoService
    {
        private readonly ApplicationDbContext _context;

        public VehiculoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<VehiculoDTO>> ObtenerTodosLosVehiculosAsync()
        {
            try
            {
                Log.Information("Obteniendo todos los vehículos.");
                var vehiculos = await _context.Vehiculos.ToListAsync();
                Log.Information("Se obtuvieron {Count} vehículos.", vehiculos.Count);
                return vehiculos.Select(v => new VehiculoDTO
                {
                    Id_Vehiculo = v.Id_Vehiculo,
                    Marca = v.Marca,
                    Modelo = v.Modelo,
                    Anio = v.Anio,
                    Imagen = v.Imagen,
                    Descripcion = v.Descripcion,
                    Placa = v.Placa,
                    Kilometraje = v.Kilometraje,
                    FechaRegistro = v.FechaRegistro,
                    CostoTemporadaAlta = v.CostoTemporadaAlta,
                    CostoTemporadaBaja = v.CostoTemporadaBaja,
                    IsRentado = v.IsRentado,
                    IsMantenimiento = v.IsMantenimiento,
                    Usuarios_id = v.Usuarios_id,
                    Categoria_Id = v.Categoria_Id,
                    IsAutomatico = v.IsAutomatico
                }).ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener todos los vehículos.");
                throw;
            }
        }

        public async Task<VehiculoDetallesDto> ObtenerVehiculoPorIdAsync(int id)
        {
            try
            {
                Log.Information("Obteniendo vehículo con ID {VehiculoId}.", id);
                var vehiculo = await _context.Vehiculos.FindAsync(id);
                if (vehiculo == null)
                {
                    Log.Warning("Vehículo con ID {VehiculoId} no encontrado.", id);
                    return null;
                }

                Log.Information("Vehículo con ID {VehiculoId} encontrado.", id);
                return new VehiculoDetallesDto
                {
                    Id_Vehiculo = vehiculo.Id_Vehiculo,
                    Marca = vehiculo.Marca,
                    Modelo = vehiculo.Modelo,
                    Anio = vehiculo.Anio,
                    Imagen = vehiculo.Imagen,
                    Usuarios_id = vehiculo.Usuarios_id,
                    Descripcion = vehiculo.Descripcion,
                    Placa = vehiculo.Placa,
                    Kilometraje = vehiculo.Kilometraje,
                    FechaRegistro = vehiculo.FechaRegistro,
                    CostoTemporadaAlta = vehiculo.CostoTemporadaAlta,
                    CostoTemporadaBaja = vehiculo.CostoTemporadaBaja,
                    IsRentado = vehiculo.IsRentado,
                    IsAutomatico = vehiculo.IsAutomatico,
                    Categoria_Id = vehiculo.Categoria_Id,
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener el vehículo con ID {VehiculoId}.", id);
                throw;
            }
        }

        public async Task CrearVehiculoAsync(VehiculoDTO nuevoVehiculoDTO)
        {
            // Validar si ya existe una placa igual
            var placaExiste = await _context.Vehiculos.AnyAsync(v => v.Placa == nuevoVehiculoDTO.Placa);
            if (placaExiste)
                throw new Exception("Ya existe un vehículo con la misma placa.");

            var nuevoVehiculo = new Vehiculo
            {
                Marca = nuevoVehiculoDTO.Marca,
                Modelo = nuevoVehiculoDTO.Modelo,
                Anio = nuevoVehiculoDTO.Anio,
                Imagen = nuevoVehiculoDTO.Imagen,
                Descripcion = nuevoVehiculoDTO.Descripcion,
                Placa = nuevoVehiculoDTO.Placa,
                Kilometraje = nuevoVehiculoDTO.Kilometraje,
                FechaRegistro = nuevoVehiculoDTO.FechaRegistro, // Asegúrate de asignarlo.
                CostoTemporadaAlta = nuevoVehiculoDTO.CostoTemporadaAlta,
                CostoTemporadaBaja = nuevoVehiculoDTO.CostoTemporadaBaja,
                IsRentado = nuevoVehiculoDTO.IsRentado,
                IsAutomatico = nuevoVehiculoDTO.IsAutomatico,
                Usuarios_id = nuevoVehiculoDTO.Usuarios_id,
                Categoria_Id = nuevoVehiculoDTO.Categoria_Id
            };

            await _context.Vehiculos.AddAsync(nuevoVehiculo);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ActualizarVehiculoAsync(int id, VehiculoDTO vehiculoActualizadoDTO)
        {
            var vehiculoExistente = await _context.Vehiculos.FindAsync(id);
            if (vehiculoExistente == null)
                return false;
            // Validar si otra placa duplicada existe
            var placaDuplicada = await _context.Vehiculos
                .AnyAsync(v => v.Placa == vehiculoActualizadoDTO.Placa && v.Id_Vehiculo != id);
            if (placaDuplicada)
                throw new Exception("Ya existe otro vehículo con la misma placa.");

            vehiculoExistente.Marca = vehiculoActualizadoDTO.Marca;
            vehiculoExistente.Modelo = vehiculoActualizadoDTO.Modelo;
            vehiculoExistente.Anio = vehiculoActualizadoDTO.Anio;
            vehiculoExistente.Imagen = vehiculoActualizadoDTO.Imagen;
            vehiculoExistente.Descripcion = vehiculoActualizadoDTO.Descripcion;
            vehiculoExistente.Placa = vehiculoActualizadoDTO.Placa;
            vehiculoExistente.Kilometraje = vehiculoActualizadoDTO.Kilometraje;
            vehiculoExistente.FechaRegistro = vehiculoActualizadoDTO.FechaRegistro; // Permitir modificarlo.
            vehiculoExistente.CostoTemporadaAlta = vehiculoActualizadoDTO.CostoTemporadaAlta;
            vehiculoExistente.CostoTemporadaBaja = vehiculoActualizadoDTO.CostoTemporadaBaja;
            vehiculoExistente.IsRentado = vehiculoActualizadoDTO.IsRentado;
            vehiculoExistente.IsAutomatico = vehiculoActualizadoDTO.IsAutomatico;
            // Asegúrate de que estos campos sean actualizados también
            vehiculoExistente.Usuarios_id = vehiculoActualizadoDTO.Usuarios_id;
            vehiculoExistente.Categoria_Id = vehiculoActualizadoDTO.Categoria_Id;

            _context.Vehiculos.Update(vehiculoExistente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarVehiculoAsync(int id)
        {
            try
            {
                Log.Information("Eliminando vehículo con ID {VehiculoId}.", id);
                var vehiculo = await _context.Vehiculos.FindAsync(id);
                if (vehiculo == null)
                {
                    Log.Warning("Vehículo con ID {VehiculoId} no encontrado para eliminar.", id);
                    return false;
                }

                var vehiculosSeguros = await _context.VehiculosSeguros
                    .Where(vs => vs.Vehiculos_id == id)
                    .ToListAsync();

                if (vehiculosSeguros.Any())
                {
                    _context.VehiculosSeguros.RemoveRange(vehiculosSeguros);
                }

                _context.Vehiculos.Remove(vehiculo);
                await _context.SaveChangesAsync();
                Log.Information("Vehículo con ID {VehiculoId} eliminado exitosamente.", id);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al eliminar el vehículo con ID {VehiculoId}.", id);
                throw;
            }
        }

        public async Task<List<VehiculoUsuarioDto>> ObtenerVehiculosPorUsuarioAsync(int usuarioId)
        {
            try
            {
                Log.Information("Obteniendo vehículos para el usuario con ID {UsuarioId}.", usuarioId);
                var vehiculos = await _context.Vehiculos
                    .Where(v => v.Usuarios_id == usuarioId)
                    .ToListAsync();

                var vehiculosDto = vehiculos.Select(v => new VehiculoUsuarioDto
                {
                    Id_Vehiculo = v.Id_Vehiculo,
                    Marca = v.Marca,
                    Modelo = v.Modelo,
                    Placa = v.Placa,
                    Usuarios_id = v.Usuarios_id,
                    IsAutomatico = v.IsAutomatico,
                    CostoTemporadaAlta = v.CostoTemporadaAlta,
                    CostoTemporadaBaja = v.CostoTemporadaBaja
                }).ToList();

                Log.Information("Se encontraron {Count} vehículos para el usuario con ID {UsuarioId}.", vehiculosDto.Count, usuarioId);
                return vehiculosDto;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener vehículos para el usuario con ID {UsuarioId}.", usuarioId);
                throw;
            }
        }
    }
}
