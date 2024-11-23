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
            return await _context.Seguros.ToListAsync();
        }

        // Método CREATE: Crear un nuevo seguro
        public async Task<Seguro> CrearSeguroAsync(Seguro seguro)
        {
            await _context.Seguros.AddAsync(seguro);
            await _context.SaveChangesAsync();
            return seguro;
        }

        // Método UPDATE: Actualizar un seguro existente
        public async Task<Seguro> UpdateSeguroAsync(int id, Seguro seguro)
        {
            var seguroExistente = await _context.Seguros.FindAsync(id);

            if (seguroExistente == null)
            {
                return null; // No se encontró el seguro
            }

            seguroExistente.TipoSeguro = seguro.TipoSeguro;
            seguroExistente.FechaInicio = seguro.FechaInicio;
            seguroExistente.FechaVencimiento = seguro.FechaVencimiento;
            seguroExistente.AutorContratado = seguro.AutorContratado;
            seguroExistente.Monto = seguro.Monto;

            await _context.SaveChangesAsync();
            return seguroExistente;
        }

        // Método DELETE: Eliminar un seguro
        public async Task<bool> DeleteSeguroAsync(int id)
        {
            // Buscar el vehículo por su ID
            var seguro = await _context.Seguros.FindAsync(id);
            if (seguro == null)
                return false;

            // Buscar los registros asociados en VehiculosSeguros
            var vehiculosSeguros = await _context.VehiculosSeguros
                .Where(s => s.Seguros_id == id)
                .ToListAsync();

            // Eliminar cada registro relacionado en VehiculosSeguros
            if (vehiculosSeguros.Any())
            {
                _context.VehiculosSeguros.RemoveRange(vehiculosSeguros);
            }

            // Finalmente, eliminar el vehículo
            _context.Seguros.Remove(seguro);

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
