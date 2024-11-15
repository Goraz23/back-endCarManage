using BackCar.Application.DTOs;
using BackCar.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
            var contratos = await _contratoRentaService.ObtenerTodosLosContratosAsync();
            return Ok(contratos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContratoRentaDTO>> GetContrato(int id)
        {
            var contrato = await _contratoRentaService.ObtenerContratoPorIdAsync(id);
            if (contrato == null) return NotFound();

            return Ok(contrato);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ContratoRentaDTO nuevoContrato)
        {
            if (nuevoContrato == null) return BadRequest("Datos inválidos");

            await _contratoRentaService.CrearContratoAsync(nuevoContrato);
            return CreatedAtAction(nameof(GetContrato), new { id = nuevoContrato.Id_ContratoRenta }, nuevoContrato);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _contratoRentaService.EliminarContratoAsync(id);
            if (!eliminado) return NotFound("Contrato no encontrado");

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ContratoRentaDTO contratoActualizado)
        {
            if (contratoActualizado == null || contratoActualizado.Id_ContratoRenta != id)
                return BadRequest("Datos inválidos");

            var actualizado = await _contratoRentaService.ActualizarContratoAsync(id, contratoActualizado);
            if (!actualizado) return NotFound("Contrato no encontrado");

            return NoContent();
        }
    }
}
