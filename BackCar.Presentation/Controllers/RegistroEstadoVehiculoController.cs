using BackCar.Application.Interfaces;
using BackCar._Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackCar.Application.DTOs;
using Serilog;

namespace BackCar.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroEstadoVehiculoController : ControllerBase
    {
        private readonly IRegistroEstadoVehiculoService _service;

        public RegistroEstadoVehiculoController(IRegistroEstadoVehiculoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<RegistroEstadoVehiculo>>> GetAll()
        {
            Log.Information("Inicio de la solicitud para obtener todos los registros de estado de vehículos.");
            try
            {
                var registros = await _service.ObtenerTodosAsync();
                Log.Information("Registros obtenidos con éxito. Total: {Count}", registros.Count);
                return Ok(registros);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener todos los registros de estado de vehículos.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<RegistroEstadoVehiculo>> Create([FromBody] RegistroEstadoVehiculoDto registroDto)
        {
            Log.Information("Inicio de la solicitud para crear un nuevo registro de estado de vehículo.");
            try
            {
                var registro = new RegistroEstadoVehiculo
                {
                    KilometrajeInicial = registroDto.KilometrajeInicial,
                    KilometrajeFinal = registroDto.KilometrajeFinal,
                    DetallesRetorno = registroDto.DetallesRetorno,
                    AplicanCargos = registroDto.AplicanCargos,
                    Id_ContratoRenta = registroDto.Id_ContratoRenta
                };

                var nuevoRegistro = await _service.CrearAsync(registro);
                Log.Information("Registro creado con éxito. ID: {Id}", nuevoRegistro.Id_RegistroEstadoVehiculo);
                return CreatedAtAction(nameof(GetAll), new { id = nuevoRegistro.Id_RegistroEstadoVehiculo }, nuevoRegistro);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al crear un registro de estado de vehículo.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RegistroEstadoVehiculoDto registroDto)
        {
            Log.Information("Inicio de la solicitud para actualizar el registro de estado de vehículo con ID: {Id}", id);
            try
            {
                var registro = new RegistroEstadoVehiculo
                {
                    Id_RegistroEstadoVehiculo = id,
                    KilometrajeInicial = registroDto.KilometrajeInicial,
                    KilometrajeFinal = registroDto.KilometrajeFinal,
                    DetallesRetorno = registroDto.DetallesRetorno,
                    AplicanCargos = registroDto.AplicanCargos,
                    Id_ContratoRenta = registroDto.Id_ContratoRenta
                };

                var actualizado = await _service.ActualizarAsync(id, registro);
                if (actualizado == null)
                {
                    Log.Warning("No se encontró el registro de estado de vehículo con ID: {Id}", id);
                    return NotFound();
                }

                Log.Information("Registro actualizado con éxito. ID: {Id}", id);
                return Ok(actualizado);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al actualizar el registro de estado de vehículo con ID: {Id}", id);
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Log.Information("Inicio de la solicitud para eliminar el registro de estado de vehículo con ID: {Id}", id);
            try
            {
                var eliminado = await _service.EliminarAsync(id);
                if (!eliminado)
                {
                    Log.Warning("No se encontró el registro de estado de vehículo con ID: {Id}", id);
                    return NotFound();
                }

                Log.Information("Registro eliminado con éxito. ID: {Id}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al eliminar el registro de estado de vehículo con ID: {Id}", id);
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}
