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
    public class IncidenteService : IIncidenteService
    {
        private readonly ApplicationDbContext _context;

        public IncidenteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Incidente>> ObtenerTodosAsync()
        {
            return await _context.Incidentes.Include(i => i.ContratoRenta).ToListAsync();
        }

        public async Task<Incidente> ObtenerPorIdAsync(int id)
        {
            return await _context.Incidentes.Include(i => i.ContratoRenta)
                                            .FirstOrDefaultAsync(i => i.Id_Incidente == id);
        }

        public async Task<Incidente> CrearAsync(Incidente incidente)
        {
            _context.Incidentes.Add(incidente);
            await _context.SaveChangesAsync();
            return incidente;
        }

        public async Task<Incidente> ActualizarAsync(int id, Incidente incidente)
        {
            var existente = await _context.Incidentes.FindAsync(id);
            if (existente == null)
                return null;

            existente.FechaIncidente = incidente.FechaIncidente;
            existente.Descripcion = incidente.Descripcion;
            existente.AplicoSeguro = incidente.AplicoSeguro;
            existente.Id_ContratoRenta = incidente.Id_ContratoRenta;

            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var incidente = await _context.Incidentes.FindAsync(id);
            if (incidente == null) return false;

            _context.Incidentes.Remove(incidente);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
