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
            var usuarios = await _usuarioService.ObtenerTodosLosUsuariosAsync();
            return Ok(usuarios);
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> CreateUsuario([FromBody] UsuarioDto usuarioDto)
        {
            if (usuarioDto == null)
            {
                return BadRequest("El usuario no puede ser nulo.");
            }

            // Buscar el rol desde la base de datos usando el roles_Usuarios_id
            var rol = await context.Roles.FirstOrDefaultAsync(r => r.Id_Rol == usuarioDto.Roles_Usuarios_id);
            if (rol == null)
            {
                return BadRequest("El rol especificado no existe.");
            }

            // Mapear el DTO al modelo de entidad Usuario
            var usuario = new Usuario
            {
                Nombre = usuarioDto.Nombre,
                Correo = usuarioDto.Correo,
                Contrasenia = usuarioDto.Contrasenia,
                Telefono = usuarioDto.Telefono,
                Roles_Usuarios_id = usuarioDto.Roles_Usuarios_id,
                Rol = rol,  // Asignar el rol correspondiente
                FechaRegistro = DateTime.Now // Establecer la fecha de registro
            };

            // Crear el usuario
            var nuevoUsuario = await _usuarioService.CrearUsuarioAsync(usuario);
            return CreatedAtAction(nameof(GetUsuarios), new { id = nuevoUsuario.Id_Usuario }, nuevoUsuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UsuarioDto usuarioDto)
        {
            // Buscar el usuario a actualizar
            var usuario = await context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound(); // Si no se encuentra el usuario
            }

            // Buscar el rol desde la base de datos
            var rol = await context.Roles.FirstOrDefaultAsync(r => r.Id_Rol == usuarioDto.Roles_Usuarios_id);
            if (rol == null)
            {
                return BadRequest("El rol especificado no existe.");
            }

            // Mapear el DTO al modelo de entidad Usuario
            usuario.Nombre = usuarioDto.Nombre;
            usuario.Correo = usuarioDto.Correo;
            usuario.Contrasenia = usuarioDto.Contrasenia;
            usuario.Telefono = usuarioDto.Telefono;
            usuario.Roles_Usuarios_id = usuarioDto.Roles_Usuarios_id;
            usuario.Rol = rol;

            // Actualizar el usuario
            var usuarioActualizado = await _usuarioService.UpdateUsuarioAsync(id, usuario);

            if (usuarioActualizado == null)
            {
                return NotFound(); // Si no existe el usuario
            }

            return Ok(usuarioActualizado); // Si el usuario se actualizó correctamente
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var eliminado = await _usuarioService.DeleteUsuarioAsync(id);

            if (!eliminado)
            {
                return NotFound();  // Si no se encuentra el usuario
            }

            return NoContent();  // Si el usuario fue eliminado correctamente
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

    }
}
