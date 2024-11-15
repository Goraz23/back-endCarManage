using BackCar._Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackCar.Application.Interfaces
{
    public interface ISeguroService
    {
        Task<List<Seguro>> ObtenerTodosLosSegurosAsync(); // Método READ
        Task<Seguro> CrearSeguroAsync(Seguro seguro); // Método CREATE
        Task<Seguro> UpdateSeguroAsync(int id, Seguro seguro); // Método UPDATE
        Task<bool> DeleteSeguroAsync(int id); // Método DELETE
    }
}
