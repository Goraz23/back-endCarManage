using BackCar._Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackCar.Application.Interfaces
{
    public interface ICategoriaService
    {
        Task<List<Categoria>> ObtenerTodasLasCategoriasAsync(); // Método READ
        Task<Categoria> CrearCategoriaAsync(Categoria categoria); // Método CREATE
        Task<Categoria> UpdateCategoriaAsync(int id, Categoria categoria); // Método UPDATE
        Task<bool> DeleteCategoriaAsync(int id); // Método DELETE
    }
}

    
