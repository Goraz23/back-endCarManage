using BackCar._Domain.Entities;
using BackCar.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackCar.Application.Utils;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BackCar.Application.DTOs;
using Serilog;

namespace BackCar.Infrastructure.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtService _jwtService;

        public UsuarioService(ApplicationDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<List<Usuario>> ObtenerTodosLosUsuariosAsync()
        {
            try
            {
                Log.Information("Obteniendo todos los usuarios.");
                return await _context.Usuarios.Include(u => u.Rol).ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener los usuarios.");
                throw new Exception("Hubo un error al obtener los usuarios.");
            }
        }

        public async Task<Usuario> CrearUsuarioAsync(Usuario usuario)
        {
            try
            {
                Log.Information("Creando un nuevo usuario: {UserName}", usuario.Nombre);

                if (usuario.Roles_Usuarios_id != 0)
                {
                    var rol = await _context.Roles.FirstOrDefaultAsync(r => r.Id_Rol == usuario.Roles_Usuarios_id);
                    if (rol == null)
                    {
                        Log.Error("El rol especificado no existe.");
                        throw new ArgumentException("El rol no existe.");
                    }
                }

                usuario.Contrasenia = PasswordHasher.HashPassword(usuario.Contrasenia);
                await _context.Usuarios.AddAsync(usuario);
                await _context.SaveChangesAsync();

                Log.Information("Usuario creado exitosamente: {UserName}", usuario.Nombre);
                return usuario;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al crear el usuario.");
                throw new Exception("Hubo un error al crear el usuario.");
            }
        }

        public async Task<Usuario> UpdateUsuarioAsync(int id, Usuario usuario)
        {
            try
            {
                Log.Information("Actualizando usuario con ID: {UserId}", id);

                var usuarioExistente = await _context.Usuarios.FindAsync(id);
                if (usuarioExistente == null)
                {
                    Log.Error("Usuario con ID {UserId} no encontrado", id);
                    return null;
                }

                usuarioExistente.Nombre = usuario.Nombre;
                usuarioExistente.Correo = usuario.Correo;
                usuarioExistente.Contrasenia = usuario.Contrasenia;
                usuarioExistente.Telefono = usuario.Telefono;
                usuarioExistente.FechaRegistro = usuario.FechaRegistro;
                usuarioExistente.Roles_Usuarios_id = usuario.Roles_Usuarios_id;

                await _context.SaveChangesAsync();
                Log.Information("Usuario con ID {UserId} actualizado exitosamente", id);

                return usuarioExistente;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al actualizar el usuario con ID: {UserId}", id);
                throw new Exception($"Hubo un error al actualizar el usuario con ID {id}.");
            }
        }

        public async Task<bool> DeleteUsuarioAsync(int id)
        {
            try
            {
                Log.Information("Eliminando usuario con ID: {UserId}", id);

                var usuario = await _context.Usuarios.FindAsync(id);
                if (usuario == null)
                {
                    Log.Error("Usuario con ID {UserId} no encontrado", id);
                    return false;
                }

                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
                Log.Information("Usuario con ID {UserId} eliminado exitosamente", id);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al eliminar el usuario con ID: {UserId}", id);
                throw new Exception($"Hubo un error al eliminar el usuario con ID {id}.");
            }
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {
            try
            {
                Log.Information("Intentando login con correo: {UserEmail}", loginDto.Correo);

                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == loginDto.Correo);
                if (usuario == null || !PasswordHasher.VerifyPassword(loginDto.Contrasenia, usuario.Contrasenia))
                {
                    Log.Warning("Credenciales inválidas para el correo: {UserEmail}", loginDto.Correo);
                    throw new UnauthorizedAccessException("Credenciales inválidas");
                }

                Log.Information("Login exitoso para el correo: {UserEmail}", loginDto.Correo);
                return new LoginResponseDto
                {
                    Token = _jwtService.GenerateToken(usuario),
                    Nombre = usuario.Nombre,
                    Rol = usuario.Roles_Usuarios_id == 1 ? "Admin" : "Socio"
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error en el proceso de login con correo: {UserEmail}", loginDto.Correo);
                throw new Exception("Hubo un error en el proceso de login.");
            }
        }
    }
}
