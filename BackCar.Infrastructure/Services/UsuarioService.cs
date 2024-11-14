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
        //Método CREATE para Usuarios
        public async Task<Usuario> CrearUsuarioAsync(Usuario usuario)
        {
            // Verificar que usuario jale
            if (usuario.Roles_Usuarios_id != 0)
            {
                //Verifica si usuario existe antes de crear;
                var rol = await _context.Roles.FirstOrDefaultAsync(r => r.Id_Rol == usuario.Roles_Usuarios_id);
                if (rol == null)
                {
                    throw new ArgumentException("El rol no existe.");
                }
            }

            // Añadir usuario
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return usuario; // Devolver el usuario creado
        }

        //Método UPDATE:
        public async Task<Usuario> UpdateUsuarioAsync(int id, Usuario usuario)
        {
            // Buscar el usuario existente por su Id
            var usuarioExistente = await _context.Usuarios.FindAsync(id);

            if (usuarioExistente == null)
            {
                // Si no se encuentra, devolver null (o puedes lanzar una excepción)
                return null;
            }

            // Si el usuario existe, actualizamos sus propiedades
            usuarioExistente.Nombre = usuario.Nombre;
            usuarioExistente.Correo = usuario.Correo;
            usuarioExistente.Contrasenia = usuario.Contrasenia;
            usuarioExistente.Telefono = usuario.Telefono;
            usuarioExistente.FechaRegistro = usuario.FechaRegistro;
            usuarioExistente.Roles_Usuarios_id = usuario.Roles_Usuarios_id;

            // Guardamos los cambios en la base de datos
            await _context.SaveChangesAsync();

            return usuarioExistente;  // Devolvemos el usuario actualizado
        }

        //Método DELETE:
        public async Task<bool> DeleteUsuarioAsync(int id)
        {
            // Buscar el usuario por Id
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                // Si no se encuentra el usuario, devolvemos false
                return false;
            }

            // Si se encuentra, lo eliminamos
            _context.Usuarios.Remove(usuario);

            // Guardamos los cambios en la base de datos
            await _context.SaveChangesAsync();

            return true;  // Devolvemos true indicando que el usuario fue eliminado
        }
    }
}
