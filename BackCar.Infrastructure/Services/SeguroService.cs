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
            var seguro = await _context.Seguros.FindAsync(id);

            if (seguro == null)
            {
                return false; // No se encontró el seguro
            }

            _context.Seguros.Remove(seguro);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
