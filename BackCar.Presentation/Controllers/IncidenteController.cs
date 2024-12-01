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
        public async Task<ActionResult<List<Incidente>>> GetIncidentes()
        {
            try
            {
                var incidentes = await _incidenteService.ObtenerTodosAsync();
                Log.Information("Se obtuvieron todos los incidentes desde el endpoint.");
                return Ok(incidentes);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener todos los incidentes desde el endpoint.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Incidente>> GetIncidente(int id)
        {
            try
            {
                var incidente = await _incidenteService.ObtenerPorIdAsync(id);
                if (incidente == null)
                {
                    Log.Warning("Incidente con ID {Id} no encontrado en el endpoint.", id);
                    return NotFound();
                }

                Log.Information("Incidente con ID {Id} obtenido desde el endpoint.", id);
                return Ok(incidente);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener el incidente con ID {Id} desde el endpoint.", id);
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Incidente>> CreateIncidente([FromBody] IncidenteDto incidenteDto)
        {
            try
            {
                if (incidenteDto == null)
                {
                    Log.Warning("Datos inválidos para crear un incidente en el endpoint.");
                    return BadRequest("Datos inválidos.");
                }

                var incidente = new Incidente
                {
                    FechaIncidente = incidenteDto.FechaIncidente,
                    Descripcion = incidenteDto.Descripcion,
                    AplicoSeguro = incidenteDto.AplicoSeguro,
                    Id_ContratoRenta = incidenteDto.Id_ContratoRenta
                };

                var nuevoIncidente = await _incidenteService.CrearAsync(incidente);
                Log.Information("Incidente creado correctamente desde el endpoint.");
                return CreatedAtAction(nameof(GetIncidente), new { id = nuevoIncidente.Id_Incidente }, nuevoIncidente);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al crear un nuevo incidente desde el endpoint.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIncidente(int id, [FromBody] IncidenteDto incidenteDto)
        {
            try
            {
                if (incidenteDto == null || incidenteDto.Id_ContratoRenta != id)
                {
                    Log.Warning("Datos inválidos para actualizar el incidente con ID {Id} en el endpoint.", id);
                    return BadRequest("Datos inválidos.");
                }

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
                    Log.Warning("Incidente con ID {Id} no encontrado para actualización en el endpoint.", id);
                    return NotFound();
                }

                Log.Information("Incidente con ID {Id} actualizado correctamente desde el endpoint.", id);
                return Ok(incidenteActualizado);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al actualizar el incidente con ID {Id} desde el endpoint.", id);
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncidente(int id)
        {
            try
            {
                var eliminado = await _incidenteService.EliminarAsync(id);
                if (!eliminado)
                {
                    Log.Warning("Incidente con ID {Id} no encontrado para eliminación en el endpoint.", id);
                    return NotFound();
                }

                Log.Information("Incidente con ID {Id} eliminado correctamente desde el endpoint.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al eliminar el incidente con ID {Id} desde el endpoint.", id);
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}
