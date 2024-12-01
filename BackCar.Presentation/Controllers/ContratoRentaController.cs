using BackCar.Application.DTOs;
using BackCar.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace BackCar.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratoRentaController : ControllerBase
    {
        private readonly IContratoRentaService _contratoRentaService;

        public ContratoRentaController(IContratoRentaService contratoRentaService)
        {
            _contratoRentaService = contratoRentaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ContratoRentaDTO>>> GetContratos()
        {
            try
            {
                var contratos = await _contratoRentaService.ObtenerTodosLosContratosAsync();
                Log.Information("Contratos obtenidos en el endpoint.");
                return Ok(contratos);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener los contratos en el endpoint.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContratoRentaDTO>> GetContrato(int id)
        {
            try
            {
                var contrato = await _contratoRentaService.ObtenerContratoPorIdAsync(id);
                if (contrato == null)
                {
                    Log.Warning("Contrato con ID {Id} no encontrado en el endpoint.", id);
                    return NotFound();
                }

                Log.Information("Contrato con ID {Id} obtenido en el endpoint.", id);
                return Ok(contrato);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener el contrato con ID {Id} en el endpoint.", id);
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ContratoRentaDTO nuevoContrato)
        {
            try
            {
                if (nuevoContrato == null)
                {
                    Log.Warning("Datos de contrato inválidos recibidos en el endpoint.");
                    return BadRequest("Datos inválidos.");
                }

                await _contratoRentaService.CrearContratoAsync(nuevoContrato);
                Log.Information("Contrato creado correctamente en el endpoint.");
                return CreatedAtAction(nameof(GetContrato), new { id = nuevoContrato.Id_ContratoRenta }, nuevoContrato);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al crear un nuevo contrato en el endpoint.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var eliminado = await _contratoRentaService.EliminarContratoAsync(id);
                if (!eliminado)
                {
                    Log.Warning("Contrato con ID {Id} no encontrado para eliminación en el endpoint.", id);
                    return NotFound("Contrato no encontrado.");
                }

                Log.Information("Contrato con ID {Id} eliminado en el endpoint.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al eliminar el contrato con ID {Id} en el endpoint.", id);
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ContratoRentaDTO contratoActualizado)
        {
            try
            {
                if (contratoActualizado == null || contratoActualizado.Id_ContratoRenta != id)
                {
                    Log.Warning("Datos inválidos para actualizar el contrato con ID {Id} en el endpoint.", id);
                    return BadRequest("Datos inválidos.");
                }

                var actualizado = await _contratoRentaService.ActualizarContratoAsync(id, contratoActualizado);
                if (!actualizado)
                {
                    Log.Warning("Contrato con ID {Id} no encontrado para actualización en el endpoint.", id);
                    return NotFound("Contrato no encontrado.");
                }

                Log.Information("Contrato con ID {Id} actualizado en el endpoint.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al actualizar el contrato con ID {Id} en el endpoint.", id);
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}
