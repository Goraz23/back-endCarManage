using BackCar.Application.DTOs;
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
    public class IncidenteController : ControllerBase
    {
        private readonly IIncidenteService _incidenteService;

        public IncidenteController(IIncidenteService incidenteService)
        {
            _incidenteService = incidenteService;
        }

        [HttpGet]
        public async Task<ActionResult<List<IncidenteMapeoDto>>> GetIncidentes()
        {
            var incidentes = await _incidenteService.ObtenerTodosLosIncidentesAsync();
            return Ok(incidentes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IncidenteDto>> GetIncidente(int id)
        {
            var incidente = await _incidenteService.ObtenerIncidentePorIdAsync(id);
            if (incidente == null)
                return NotFound();

            return Ok(incidente);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIncidente([FromBody] IncidenteDto incidenteDto)
        {
            await _incidenteService.CrearIncidenteAsync(incidenteDto);
            return CreatedAtAction(nameof(GetIncidente), new { id = incidenteDto.Id_Incidente }, incidenteDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIncidente(int id, [FromBody] IncidenteDto incidenteDto)
        {
            var actualizado = await _incidenteService.ActualizarIncidenteAsync(id, incidenteDto);
            if (!actualizado)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncidente(int id)
        {
            var eliminado = await _incidenteService.EliminarIncidenteAsync(id);
            if (!eliminado)
                return NotFound();

            return NoContent();
        }

        [HttpGet("vehiculo/{vehiculoId}")]
        public async Task<ActionResult<List<IncidenteDto>>> GetIncidentesPorVehiculoId(int vehiculoId)
        {
            var incidentes = await _incidenteService.ObtenerIncidentesPorVehiculoIdAsync(vehiculoId);
            if (incidentes == null || incidentes.Count == 0)
            {
                Log.Warning("No se encontraron incidentes para el vehículo con ID {VehiculoId}.", vehiculoId);
                return NotFound();
            }

            return Ok(incidentes);
        }

    }

}
