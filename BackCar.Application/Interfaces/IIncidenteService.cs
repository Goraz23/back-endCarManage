using BackCar._Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackCar.Application.Interfaces
{
    public interface IIncidenteService
    {
        Task<List<Incidente>> ObtenerTodosAsync();
        Task<Incidente> ObtenerPorIdAsync(int id);
        Task<Incidente> CrearAsync(Incidente incidente);
        Task<Incidente> ActualizarAsync(int id, Incidente incidente);
        Task<bool> EliminarAsync(int id);
    }
}
