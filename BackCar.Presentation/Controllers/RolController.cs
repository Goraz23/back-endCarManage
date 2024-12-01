using BackCar.Application.Interfaces;
using BackCar._Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace BackCar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Rol>>> GetRoles()
        {
            try
            {
                Log.Information("Obteniendo todos los roles...");
                var roles = await _rolService.ObtenerTodosLosRolesAsync();
                Log.Information("Roles obtenidos con �xito.");
                return Ok(roles);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener los roles.");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Rol nuevoRol)
        {
            if (nuevoRol == null || string.IsNullOrEmpty(nuevoRol.Nombre))
            {
                Log.Warning("Intento de crear rol con datos inv�lidos.");
                return BadRequest("Datos inv�lidos");
            }

            try
            {
                Log.Information($"Creando rol: {nuevoRol.Nombre}...");
                await _rolService.CrearRolAsync(nuevoRol);
                Log.Information($"Rol {nuevoRol.Nombre} creado exitosamente.");
                return CreatedAtAction(nameof(GetRoles), new { id = nuevoRol.Id_Rol }, nuevoRol);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al crear el rol.");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Log.Information($"Eliminando rol con ID: {id}...");
                var eliminado = await _rolService.EliminarRolAsync(id);
                if (!eliminado)
                {
                    Log.Warning($"Rol con ID {id} no encontrado.");
                    return NotFound("Rol no encontrado");
                }

                Log.Information($"Rol con ID {id} eliminado exitosamente.");
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error al eliminar rol con ID {id}.");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Rol rolActualizado)
        {
            if (rolActualizado == null || rolActualizado.Id_Rol != id)
            {
                Log.Warning($"Datos de rol inv�lidos para ID {id}.");
                return BadRequest("Datos de rol inv�lidos");
            }

            try
            {
                Log.Information($"Actualizando rol con ID {id}...");
                var actualizado = await _rolService.ActualizarRolAsync(id, rolActualizado);
                if (!actualizado)
                {
                    Log.Warning($"Rol con ID {id} no encontrado para actualizaci�n.");
                    return NotFound("Rol no encontrado");
                }

                Log.Information($"Rol con ID {id} actualizado exitosamente.");
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error al actualizar rol con ID {id}.");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
