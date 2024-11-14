using BackCar.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackCar._Domain.Entities;
using BackCar.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using BackCar.Infrastructure;

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
        public async Task<ActionResult<Usuario>> CreateUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest("El usuario no puede ser nulo.");
            }

            // Buscar el rol desde la base de datos usando el roles_Usuarios_id
            var rol = await context.Roles.FirstOrDefaultAsync(r => r.Id_Rol == usuario.Roles_Usuarios_id);
            if (rol == null)
            {
                return BadRequest("El rol especificado no existe.");
            }

            // Asignar el rol al usuario
            usuario.Rol = rol;

            // Crear el usuario
            var nuevoUsuario = await _usuarioService.CrearUsuarioAsync(usuario);
            return CreatedAtAction(nameof(GetUsuarios), new { id = nuevoUsuario.Id_Usuario }, nuevoUsuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, Usuario usuario)
        {
            var usuarioActualizado = await _usuarioService.UpdateUsuarioAsync(id, usuario);

            if (usuarioActualizado == null)
            {
                return NotFound();  // Si no existe el usuario
            }

            return Ok(usuarioActualizado);  // Si el usuario se actualizó correctamente
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
    }
}
