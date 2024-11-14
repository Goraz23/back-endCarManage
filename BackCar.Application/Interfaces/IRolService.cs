using BackCar._Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackCar.Application.Interfaces
{
    public interface IRolService
    {
        Task<List<Rol>> ObtenerTodosLosRolesAsync();
        //Método Post:
        Task CrearRolAsync(Rol nuevoRol);

        Task<bool> EliminarRolAsync(int id);

        Task<bool> ActualizarRolAsync(int id, Rol rolActualizado);  // Nuevo método
    }
}
