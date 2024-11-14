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
    }
}
