using BackCar.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackCar._Domain.Entities;
using BackCar.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using BackCar.Infrastructure;
using BackCar.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Serilog;

namespace BackCar.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ApplicationDbContext context;

        public UsuarioController(IUsuarioService usuarioService, ApplicationDbContext _context)
        {
            _usuarioService = usuarioService;
            context = _context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> GetUsuarios()
        {
            try
            {
                Log.Information("Obteniendo todos los usuarios.");
                var usuarios = await _usuarioService.ObtenerTodosLosUsuariosAsync();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener los usuarios.");
                return StatusCode(500, "Hubo un error al obtener los usuarios.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> CreateUsuario([FromBody] UsuarioDto usuarioDto)
        {
            try
            {
                Log.Information("Creando un nuevo usuario con correo: {UserEmail}", usuarioDto.Correo);

                if (usuarioDto == null)
                {
                    Log.Warning("El usuario no puede ser nulo.");
                    return BadRequest("El usuario no puede ser nulo.");
                }

                var rol = await context.Roles.FirstOrDefaultAsync(r => r.Id_Rol == usuarioDto.Roles_Usuarios_id);
                if (rol == null)
                {
                    Log.Warning("El rol especificado no existe.");
                    return BadRequest("El rol especificado no existe.");
                }

                var usuario = new Usuario
                {
                    Nombre = usuarioDto.Nombre,
                    Correo = usuarioDto.Correo,
                    Contrasenia = usuarioDto.Contrasenia,
                    Telefono = usuarioDto.Telefono,
                    Roles_Usuarios_id = usuarioDto.Roles_Usuarios_id,
                    Rol = rol,
                    FechaRegistro = DateTime.Now
                };

                var nuevoUsuario = await _usuarioService.CrearUsuarioAsync(usuario);
                Log.Information("Usuario creado con ID: {UserId}", nuevoUsuario.Id_Usuario);
                return CreatedAtAction(nameof(GetUsuarios), new { id = nuevoUsuario.Id_Usuario }, nuevoUsuario);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al crear el usuario con correo: {UserEmail}", usuarioDto.Correo);
                return StatusCode(500, "Hubo un error al crear el usuario.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UsuarioDto usuarioDto)
        {
            try
            {
                Log.Information("Actualizando usuario con ID: {UserId}", id);

                var usuario = await context.Usuarios.FindAsync(id);
                if (usuario == null)
                {
                    Log.Error("Usuario con ID {UserId} no encontrado", id);
                    return NotFound();
                }

                usuario.Nombre = usuarioDto.Nombre;
                usuario.Telefono = usuarioDto.Telefono;
                usuario.Correo = usuarioDto.Correo;

                await _usuarioService.UpdateUsuarioAsync(id, usuario);

                Log.Information("Usuario con ID {UserId} actualizado exitosamente", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al actualizar el usuario con ID: {UserId}", id);
                return StatusCode(500, "Hubo un error al actualizar el usuario.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            try
            {
                Log.Information("Eliminando usuario con ID: {UserId}", id);
                var resultado = await _usuarioService.DeleteUsuarioAsync(id);

                if (!resultado)
                {
                    Log.Error("No se pudo eliminar el usuario con ID {UserId}", id);
                    return NotFound();
                }

                Log.Information("Usuario con ID {UserId} eliminado exitosamente", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al eliminar el usuario con ID: {UserId}", id);
                return StatusCode(500, "Hubo un error al eliminar el usuario.");
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var loginResponse = await _usuarioService.LoginAsync(loginDto);
                return Ok(loginResponse);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Credenciales inválidas");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuarioPorId(int id)
        {
            try
            {
                Log.Information("Obteniendo usuario con ID: {UserId}", id);

                var usuario = await _usuarioService.ObtenerUsuarioPorIdAsync(id);

                if (usuario == null)
                {
                    Log.Warning("Usuario con ID {UserId} no encontrado", id);
                    return NotFound($"No se encontró un usuario con ID {id}");
                }

                Log.Information("Usuario con ID {UserId} obtenido exitosamente", id);
                return Ok(usuario);
            }
            catch (KeyNotFoundException ex)
            {
                Log.Warning(ex, "Usuario con ID {UserId} no encontrado", id);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener el usuario con ID: {UserId}", id);
                return StatusCode(500, "Hubo un error al obtener el usuario.");
            }
        }



    }
}
