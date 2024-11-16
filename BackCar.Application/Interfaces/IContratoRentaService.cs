using BackCar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackCar.Application.Interfaces
{
    public interface IContratoRentaService
    {
        Task<List<ContratoRentaDTO>> ObtenerTodosLosContratosAsync();
        Task<ContratoRentaDTO> ObtenerContratoPorIdAsync(int id);
        Task CrearContratoAsync(ContratoRentaDTO nuevoContrato);
        Task<bool> EliminarContratoAsync(int id);
        Task<bool> ActualizarContratoAsync(int id, ContratoRentaDTO contratoActualizado);
    }
}
