using BackCar._Domain.Entities;
using BackCar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackCar.Application.Interfaces
{
    public interface IIncidenteService
    {
        Task<List<IncidenteMapeoDto>> ObtenerTodosLosIncidentesAsync();
        Task<IncidenteDto> ObtenerIncidentePorIdAsync(int id);
        Task CrearIncidenteAsync(IncidenteDto nuevoIncidente);
        Task<bool> ActualizarIncidenteAsync(int id, IncidenteDto incidenteActualizado);
        Task<bool> EliminarIncidenteAsync(int id);
        Task<List<IncidenteDto>> ObtenerIncidentesPorVehiculoIdAsync(int vehiculoId);

    }
}
