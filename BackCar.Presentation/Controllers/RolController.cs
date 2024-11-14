using BackCar.Application.Interfaces;
using BackCar._Domain.Entities;
using Microsoft.AspNetCore.Mvc;

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
            var roles = await _rolService.ObtenerTodosLosRolesAsync();
            return Ok(roles);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Rol nuevoRol)
        {
            if (nuevoRol == null || string.IsNullOrEmpty(nuevoRol.Nombre))
                return BadRequest("Datos inválidos");

            await _rolService.CrearRolAsync(nuevoRol);
            return CreatedAtAction(nameof(GetRoles), new { id = nuevoRol.Id_Rol }, nuevoRol);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _rolService.EliminarRolAsync(id);
            if (!eliminado)
                return NotFound("Rol no encontrado");

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Rol rolActualizado)
        {
            if (rolActualizado == null || rolActualizado.Id_Rol != id)
                return BadRequest("Datos de rol inválidos");

            var actualizado = await _rolService.ActualizarRolAsync(id, rolActualizado);
            if (!actualizado)
                return NotFound("Rol no encontrado");

            return NoContent();
        }
    }
}
