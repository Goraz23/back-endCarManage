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
    public class RegistroEstadoVehiculoService : IRegistroEstadoVehiculoService
    {
        private readonly ApplicationDbContext _context;

        public RegistroEstadoVehiculoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<RegistroEstadoVehiculo>> ObtenerTodosAsync()
        {
            return await _context.RegistroEstadoVehiculos.Include(r => r.ContratoRenta).ToListAsync();
        }

        public async Task<RegistroEstadoVehiculo> CrearAsync(RegistroEstadoVehiculo registro)
        {
            await _context.RegistroEstadoVehiculos.AddAsync(registro);
            await _context.SaveChangesAsync();
            return registro;
        }

        public async Task<RegistroEstadoVehiculo> ActualizarAsync(int id, RegistroEstadoVehiculo registro)
        {
            var registroExistente = await _context.RegistroEstadoVehiculos.FindAsync(id);

            if (registroExistente == null) return null;

            registroExistente.KilometrajeInicial = registro.KilometrajeInicial;
            registroExistente.KilometrajeFinal = registro.KilometrajeFinal;
            registroExistente.DetallesRetorno = registro.DetallesRetorno;
            registroExistente.AplicanCargos = registro.AplicanCargos;
            registroExistente.Id_ContratoRenta = registro.Id_ContratoRenta;

            await _context.SaveChangesAsync();
            return registroExistente;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var registro = await _context.RegistroEstadoVehiculos.FindAsync(id);

            if (registro == null) return false;

            _context.RegistroEstadoVehiculos.Remove(registro);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
