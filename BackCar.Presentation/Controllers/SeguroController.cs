using BackCar.Application.Interfaces;
using BackCar._Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;

namespace BackCar.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeguroController : ControllerBase
    {
        private readonly ISeguroService _seguroService;

        public SeguroController(ISeguroService seguroService)
        {
            _seguroService = seguroService;
        }

        // Método GET: Obtener todos los seguros
        [HttpGet]
        public async Task<ActionResult<List<Seguro>>> GetSeguros()
        {
            try
            {
                Log.Information("Obteniendo todos los seguros...");
                var seguros = await _seguroService.ObtenerTodosLosSegurosAsync();
                Log.Information("Seguros obtenidos exitosamente.");
                return Ok(seguros);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener los seguros.");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // Método POST: Crear un nuevo seguro
        [HttpPost]
        public async Task<ActionResult<Seguro>> CreateSeguro([FromBody] Seguro seguro)
        {
            if (seguro == null || string.IsNullOrEmpty(seguro.TipoSeguro))
            {
                Log.Warning("Intento de crear seguro con datos inválidos.");
                return BadRequest("El tipo de seguro no puede ser nulo o vacío.");
            }

            try
            {
                Log.Information("Creando un nuevo seguro...");
                var nuevoSeguro = await _seguroService.CrearSeguroAsync(seguro);
                Log.Information($"Seguro {nuevoSeguro.TipoSeguro} creado exitosamente.");
                return CreatedAtAction(nameof(GetSeguros), new { id = nuevoSeguro.Id_Seguro }, nuevoSeguro);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al crear el seguro.");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // Método PUT: Actualizar un seguro existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSeguro(int id, [FromBody] Seguro seguro)
        {
            if (seguro == null || seguro.Id_Seguro != id)
            {
                Log.Warning($"Datos de seguro inválidos para ID {id}.");
                return BadRequest("Datos de seguro inválidos");
            }

            try
            {
                Log.Information($"Actualizando seguro con ID {id}...");
                var seguroActualizado = await _seguroService.UpdateSeguroAsync(id, seguro);

                if (seguroActualizado == null)
                {
                    Log.Warning($"Seguro con ID {id} no encontrado para actualización.");
                    return NotFound();
                }

                Log.Information($"Seguro con ID {id} actualizado exitosamente.");
                return Ok(seguroActualizado);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error al actualizar seguro con ID {id}.");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // Método DELETE: Eliminar un seguro
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeguro(int id)
        {
            try
            {
                Log.Information($"Eliminando seguro con ID {id}...");
                var eliminado = await _seguroService.DeleteSeguroAsync(id);

                if (!eliminado)
                {
                    Log.Warning($"Seguro con ID {id} no encontrado para eliminar.");
                    return NotFound();
                }

                Log.Information($"Seguro con ID {id} eliminado exitosamente.");
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error al eliminar seguro con ID {id}.");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
