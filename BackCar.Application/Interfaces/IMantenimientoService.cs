using BackCar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackCar.Application.Interfaces
{
    public interface IMantenimientoService
    {
        Task<List<MantenimientoMapeoDto>> ObtenerTodosLosMantenimientosAsync();
        Task<MantenimientoDTO> ObtenerMantenimientoPorIdAsync(int id);
        Task CrearMantenimientoAsync(MantenimientoDTO nuevoMantenimiento);
        Task<bool> EliminarMantenimientoAsync(int id);
        Task<bool> ActualizarMantenimientoAsync(int id, MantenimientoDTO mantenimientoActualizado);
    }
}
