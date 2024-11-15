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
    public class MantenimientoService : IMantenimientoService
    {
        private readonly ApplicationDbContext _context;

        public MantenimientoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MantenimientoDTO>> ObtenerTodosLosMantenimientosAsync()
        {
            var mantenimientos = await _context.Mantenimientos
                                               .Include(m => m.Vehiculo) // Para incluir información del vehículo si es necesario
                                               .ToListAsync();

            return mantenimientos.Select(m => new MantenimientoDTO
            {
                Id_Mantenimiento = m.Id_Mantenimiento,
                Fecha = m.Fecha,
                Tipo = m.Tipo,
                Costo = m.Costo,
                Detalles = m.Detalles,
                Vehiculo_id = m.Vehiculo_id
            }).ToList();
        }

        public async Task<MantenimientoDTO> ObtenerMantenimientoPorIdAsync(int id)
        {
            var mantenimiento = await _context.Mantenimientos
                                              .Include(m => m.Vehiculo)
                                              .FirstOrDefaultAsync(m => m.Id_Mantenimiento == id);

            if (mantenimiento == null) return null;

            return new MantenimientoDTO
            {
                Id_Mantenimiento = mantenimiento.Id_Mantenimiento,
                Fecha = mantenimiento.Fecha,
                Tipo = mantenimiento.Tipo,
                Costo = mantenimiento.Costo,
                Detalles = mantenimiento.Detalles,
                Vehiculo_id = mantenimiento.Vehiculo_id
            };
        }

        public async Task CrearMantenimientoAsync(MantenimientoDTO nuevoMantenimiento)
        {
            var mantenimiento = new Mantenimiento
            {
                Fecha = nuevoMantenimiento.Fecha,
                Tipo = nuevoMantenimiento.Tipo,
                Costo = nuevoMantenimiento.Costo,
                Detalles = nuevoMantenimiento.Detalles,
                Vehiculo_id = nuevoMantenimiento.Vehiculo_id
            };

            await _context.Mantenimientos.AddAsync(mantenimiento);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EliminarMantenimientoAsync(int id)
        {
            var mantenimiento = await _context.Mantenimientos.FindAsync(id);
            if (mantenimiento == null) return false;

            _context.Mantenimientos.Remove(mantenimiento);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActualizarMantenimientoAsync(int id, MantenimientoDTO mantenimientoActualizado)
        {
            var mantenimientoExistente = await _context.Mantenimientos.FindAsync(id);
            if (mantenimientoExistente == null) return false;

            mantenimientoExistente.Fecha = mantenimientoActualizado.Fecha;
            mantenimientoExistente.Tipo = mantenimientoActualizado.Tipo;
            mantenimientoExistente.Costo = mantenimientoActualizado.Costo;
            mantenimientoExistente.Detalles = mantenimientoActualizado.Detalles;
            mantenimientoExistente.Vehiculo_id = mantenimientoActualizado.Vehiculo_id;

            _context.Mantenimientos.Update(mantenimientoExistente);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
