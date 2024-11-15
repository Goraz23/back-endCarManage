using BackCar.Application.DTOs;
using BackCar.Application.Interfaces;
using BackCar._Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackCar.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidenteController : ControllerBase
    {
        private readonly IIncidenteService _incidenteService;

        public IncidenteController(IIncidenteService incidenteService)
        {
            _incidenteService = incidenteService;
        }

        // Obtener todos los incidentes
        [HttpGet]
        public async Task<ActionResult<List<Incidente>>> GetIncidentes()
        {
            var incidentes = await _incidenteService.ObtenerTodosAsync();
            return Ok(incidentes);
        }

        // Obtener un incidente por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Incidente>> GetIncidente(int id)
        {
            var incidente = await _incidenteService.ObtenerPorIdAsync(id);
            if (incidente == null) return NotFound();
            return Ok(incidente);
        }

        // Crear un incidente
        [HttpPost]
        public async Task<ActionResult<Incidente>> CreateIncidente([FromBody] IncidenteDto incidenteDto)
        {
            var incidente = new Incidente
            {
                FechaIncidente = incidenteDto.FechaIncidente,
                Descripcion = incidenteDto.Descripcion,
                AplicoSeguro = incidenteDto.AplicoSeguro,
                Id_ContratoRenta = incidenteDto.Id_ContratoRenta
            };

            var nuevoIncidente = await _incidenteService.CrearAsync(incidente);
            return CreatedAtAction(nameof(GetIncidente), new { id = nuevoIncidente.Id_Incidente }, nuevoIncidente);
        }

        // Actualizar un incidente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIncidente(int id, [FromBody] IncidenteDto incidenteDto)
        {
            var incidente = new Incidente
            {
                Id_Incidente = id,
                FechaIncidente = incidenteDto.FechaIncidente,
                Descripcion = incidenteDto.Descripcion,
                AplicoSeguro = incidenteDto.AplicoSeguro,
                Id_ContratoRenta = incidenteDto.Id_ContratoRenta
            };

            var incidenteActualizado = await _incidenteService.ActualizarAsync(id, incidente);
            if (incidenteActualizado == null)
            {
                return NotFound();
            }

            return Ok(incidenteActualizado);
        }

        // Eliminar un incidente
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncidente(int id)
        {
            var eliminado = await _incidenteService.EliminarAsync(id);
            if (!eliminado)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
