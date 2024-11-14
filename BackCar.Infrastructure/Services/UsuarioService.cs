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
    public class UsuarioService : IUsuarioService
    {
        private readonly ApplicationDbContext _context;

        public UsuarioService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> ObtenerTodosLosUsuariosAsync()
        {
            return await _context.Usuarios.Include(u => u.Rol).ToListAsync();
        }
    }
}
