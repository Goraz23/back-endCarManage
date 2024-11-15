using BackCar.Application.Interfaces;
using BackCar._Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            var seguros = await _seguroService.ObtenerTodosLosSegurosAsync();
            return Ok(seguros);
        }

        // Método POST: Crear un nuevo seguro
        [HttpPost]
        public async Task<ActionResult<Seguro>> CreateSeguro([FromBody] Seguro seguro)
        {
            if (seguro == null || string.IsNullOrEmpty(seguro.TipoSeguro))
            {
                return BadRequest("El tipo de seguro no puede ser nulo o vacío.");
            }

            var nuevoSeguro = await _seguroService.CrearSeguroAsync(seguro);
            return CreatedAtAction(nameof(GetSeguros), new { id = nuevoSeguro.Id_Seguro }, nuevoSeguro);
        }

        // Método PUT: Actualizar un seguro existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSeguro(int id, [FromBody] Seguro seguro)
        {
            var seguroActualizado = await _seguroService.UpdateSeguroAsync(id, seguro);

            if (seguroActualizado == null)
            {
                return NotFound();
            }

            return Ok(seguroActualizado);
        }

        // Método DELETE: Eliminar un seguro
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeguro(int id)
        {
            var eliminado = await _seguroService.DeleteSeguroAsync(id);

            if (!eliminado)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
