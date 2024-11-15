using BackCar._Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackCar.Application.Interfaces
{
    public interface IClienteService
    {
        Task<List<Cliente>> ObtenerTodosLosClientesAsync(); // Método READ
        Task<Cliente> CrearClienteAsync(Cliente cliente); // Método CREATE
        Task<Cliente> UpdateClienteAsync(int id, Cliente cliente); // Método UPDATE
        Task<bool> DeleteClienteAsync(int id); // Método DELETE
    }
}
