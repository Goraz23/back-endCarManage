using BackCar._Domain.Entities;
using BackCar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackCar.Application.Interfaces
{
    public interface IVehiculosSegurosService
    {
        Task<List<VehiculosSegurosDTO>> ObtenerVehiculosSegurosAsync();

        Task CrearVehiculoSeguroAsync(VehiculosSegurosDTO vehiculosSegurosDTO);

        Task<bool> ActualizarVehiculoSeguroAsync(int id, VehiculosSegurosDTO vehiculoSeguroActualizado);

        Task<bool> EliminarVehiculoSeguroAsync(int id);
    }
}
