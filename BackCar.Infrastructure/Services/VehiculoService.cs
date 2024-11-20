using BackCar._Domain.Entities;
using BackCar.Application.DTOs;
using BackCar.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
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
            var vehiculos = await _context.Vehiculos.ToListAsync();
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
                FechaUltimoMantenimiento = v.FechaUltimoMantenimiento,
                FechaRegistro=v.FechaRegistro,
                CostoTemporadaAlta = v.CostoTemporadaAlta,
                CostoTemporadaBaja = v.CostoTemporadaBaja,
                IsRentado = v.IsRentado,
                IsMantenimiento =v.IsMantenimiento,
                Usuarios_id= v.Usuarios_id,
                Categoria_Id = v.Categoria_Id,
                IsAutomatico = v.IsAutomatico
            }).ToList();
        }

        public async Task<VehiculoDTO> ObtenerVehiculoPorIdAsync(int id)
        {
            var vehiculo = await _context.Vehiculos.FindAsync(id);
            if (vehiculo == null) return null;

            return new VehiculoDTO
            {
                Id_Vehiculo = vehiculo.Id_Vehiculo,
                Marca = vehiculo.Marca,
                Modelo = vehiculo.Modelo,
                Anio = vehiculo.Anio,
                Imagen = vehiculo.Imagen,
                Descripcion = vehiculo.Descripcion,
                Placa = vehiculo.Placa,
                Kilometraje = vehiculo.Kilometraje,
                FechaUltimoMantenimiento = vehiculo.FechaUltimoMantenimiento,
                CostoTemporadaAlta = vehiculo.CostoTemporadaAlta,
                CostoTemporadaBaja = vehiculo.CostoTemporadaBaja,
                IsRentado = vehiculo.IsRentado,
                IsAutomatico = vehiculo.IsAutomatico
            };
        }

        public async Task CrearVehiculoAsync(VehiculoDTO nuevoVehiculoDTO)
        {
            var nuevoVehiculo = new Vehiculo
            {
                Marca = nuevoVehiculoDTO.Marca,
                Modelo = nuevoVehiculoDTO.Modelo,
                Anio = nuevoVehiculoDTO.Anio,
                Imagen = nuevoVehiculoDTO.Imagen,
                Descripcion = nuevoVehiculoDTO.Descripcion,
                Placa = nuevoVehiculoDTO.Placa,
                Kilometraje = nuevoVehiculoDTO.Kilometraje,
                FechaUltimoMantenimiento = nuevoVehiculoDTO.FechaUltimoMantenimiento,
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

            vehiculoExistente.Marca = vehiculoActualizadoDTO.Marca;
            vehiculoExistente.Modelo = vehiculoActualizadoDTO.Modelo;
            vehiculoExistente.Anio = vehiculoActualizadoDTO.Anio;
            vehiculoExistente.Imagen = vehiculoActualizadoDTO.Imagen;
            vehiculoExistente.Descripcion = vehiculoActualizadoDTO.Descripcion;
            vehiculoExistente.Placa = vehiculoActualizadoDTO.Placa;
            vehiculoExistente.Kilometraje = vehiculoActualizadoDTO.Kilometraje;
            vehiculoExistente.FechaUltimoMantenimiento = vehiculoActualizadoDTO.FechaUltimoMantenimiento;
            vehiculoExistente.CostoTemporadaAlta = vehiculoActualizadoDTO.CostoTemporadaAlta;
            vehiculoExistente.CostoTemporadaBaja = vehiculoActualizadoDTO.CostoTemporadaBaja;
            vehiculoExistente.IsRentado = vehiculoActualizadoDTO.IsRentado;
            vehiculoExistente.IsAutomatico = vehiculoActualizadoDTO.IsAutomatico;

            _context.Vehiculos.Update(vehiculoExistente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarVehiculoAsync(int id)
        {
            // Buscar el vehículo por su ID
            var vehiculo = await _context.Vehiculos.FindAsync(id);
            if (vehiculo == null)
                return false;

            // Buscar los registros asociados en VehiculosSeguros
            var vehiculosSeguros = await _context.VehiculosSeguros
                .Where(vs => vs.Vehiculos_id == id)
                .ToListAsync();

            // Eliminar cada registro relacionado en VehiculosSeguros
            if (vehiculosSeguros.Any())
            {
                _context.VehiculosSeguros.RemoveRange(vehiculosSeguros);
            }

            // Finalmente, eliminar el vehículo
            _context.Vehiculos.Remove(vehiculo);

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
