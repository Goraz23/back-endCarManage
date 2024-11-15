using BackCar.Application.DTOs;
using BackCar.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackCar.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MantenimientoController : ControllerBase
    {
        private readonly IMantenimientoService _mantenimientoService;

        public MantenimientoController(IMantenimientoService mantenimientoService)
        {
            _mantenimientoService = mantenimientoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<MantenimientoDTO>>> GetMantenimientos()
        {
            var mantenimientos = await _mantenimientoService.ObtenerTodosLosMantenimientosAsync();
            return Ok(mantenimientos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MantenimientoDTO>> GetMantenimiento(int id)
        {
            var mantenimiento = await _mantenimientoService.ObtenerMantenimientoPorIdAsync(id);
            if (mantenimiento == null) return NotFound();

            return Ok(mantenimiento);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] MantenimientoDTO nuevoMantenimiento)
        {
            if (nuevoMantenimiento == null) return BadRequest("Datos inválidos");

            await _mantenimientoService.CrearMantenimientoAsync(nuevoMantenimiento);
            return CreatedAtAction(nameof(GetMantenimiento), new { id = nuevoMantenimiento.Id_Mantenimiento }, nuevoMantenimiento);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _mantenimientoService.EliminarMantenimientoAsync(id);
            if (!eliminado) return NotFound("Mantenimiento no encontrado");

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MantenimientoDTO mantenimientoActualizado)
        {
            if (mantenimientoActualizado == null || mantenimientoActualizado.Id_Mantenimiento != id)
                return BadRequest("Datos inválidos");

            var actualizado = await _mantenimientoService.ActualizarMantenimientoAsync(id, mantenimientoActualizado);
            if (!actualizado) return NotFound("Mantenimiento no encontrado");

            return NoContent();
        }
    }
}
