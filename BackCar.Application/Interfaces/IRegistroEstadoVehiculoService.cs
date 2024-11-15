using BackCar._Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackCar.Application.Interfaces
{
    public interface IRegistroEstadoVehiculoService
    {
        Task<List<RegistroEstadoVehiculo>> ObtenerTodosAsync();
        Task<RegistroEstadoVehiculo> CrearAsync(RegistroEstadoVehiculo registro);
        Task<RegistroEstadoVehiculo> ActualizarAsync(int id, RegistroEstadoVehiculo registro);
        Task<bool> EliminarAsync(int id);
    }
}
