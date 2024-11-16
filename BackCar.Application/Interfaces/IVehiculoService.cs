using BackCar._Domain.Entities;
using BackCar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackCar.Application.Interfaces
{
    public interface IVehiculoService
    {
        Task<List<VehiculoDTO>> ObtenerTodosLosVehiculosAsync();
        Task<VehiculoDTO> ObtenerVehiculoPorIdAsync(int id);
        Task CrearVehiculoAsync(VehiculoDTO nuevoVehiculoDTO);
        Task<bool> ActualizarVehiculoAsync(int id, VehiculoDTO vehiculoActualizadoDTO);
        Task<bool> EliminarVehiculoAsync(int id);
    }
}
