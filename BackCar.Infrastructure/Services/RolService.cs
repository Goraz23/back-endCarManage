using BackCar._Domain.Entities;
using BackCar.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackCar.Infrastructure.Services
{
    public class RolService : IRolService
    {
        private readonly ApplicationDbContext _context;

        public RolService(ApplicationDbContext context)
        {
            _context = context;
        }

     
        public async Task<List<Rol>> ObtenerTodosLosRolesAsync()
        {
            Console.WriteLine("Conectando a la base de datos...");  // Mensaje de depuración
            var roles = await _context.Roles.ToListAsync();
            Console.WriteLine($"Número de roles obtenidos: {roles.Count}");  // Verifica la cantidad de roles obtenidos
            return roles;
        }
    }
}
