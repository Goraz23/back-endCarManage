using BackCar._Domain.Entities;
using BackCar.Application.DTOs;
using BackCar.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace BackCar.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        private readonly IVehiculoService _vehiculoService;

        public VehiculoController(IVehiculoService vehiculoService)
        {
            _vehiculoService = vehiculoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            try
            {
                Log.Information("Iniciando solicitud para obtener todos los vehículos.");
                var vehiculos = await _vehiculoService.ObtenerTodosLosVehiculosAsync();
                Log.Information("Se completó la solicitud para obtener todos los vehículos.");
                return Ok(vehiculos);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener todos los vehículos.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                Log.Information("Iniciando solicitud para obtener vehículo con ID {VehiculoId}.", id);
                var vehiculo = await _vehiculoService.ObtenerVehiculoPorIdAsync(id);
                if (vehiculo == null)
                {
                    Log.Warning("Vehículo con ID {VehiculoId} no encontrado.", id);
                    return NotFound();
                }

                Log.Information("Se completó la solicitud para obtener el vehículo con ID {VehiculoId}.", id);
                return Ok(vehiculo);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener el vehículo con ID {VehiculoId}.", id);
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpPost("crear")]
        public async Task<IActionResult> Crear([FromBody] VehiculoDTO nuevoVehiculo)
        {
            try
            {
                Log.Information("Iniciando solicitud para crear nuevo vehículo con placa {Placa}.", nuevoVehiculo.Placa);
                await _vehiculoService.CrearVehiculoAsync(nuevoVehiculo);
                Log.Information("Se completó la solicitud para crear el vehículo con placa {Placa}.", nuevoVehiculo.Placa);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevoVehiculo.Id_Vehiculo }, nuevoVehiculo);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al crear el vehículo con placa {Placa}.", nuevoVehiculo.Placa);
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpPut("actualizar/{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] VehiculoDTO vehiculoActualizado)
        {
            try
            {
                Log.Information("Iniciando solicitud para actualizar vehículo con ID {VehiculoId}.", id);
                var resultado = await _vehiculoService.ActualizarVehiculoAsync(id, vehiculoActualizado);
                if (!resultado)
                {
                    Log.Warning("Vehículo con ID {VehiculoId} no encontrado para actualizar.", id);
                    return NotFound();
                }

                Log.Information("Se completó la solicitud para actualizar el vehículo con ID {VehiculoId}.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al actualizar el vehículo con ID {VehiculoId}.", id);
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                Log.Information("Iniciando solicitud para eliminar vehículo con ID {VehiculoId}.", id);
                var resultado = await _vehiculoService.EliminarVehiculoAsync(id);
                if (!resultado)
                {
                    Log.Warning("Vehículo con ID {VehiculoId} no encontrado para eliminar.", id);
                    return NotFound();
                }

                Log.Information("Se completó la solicitud para eliminar el vehículo con ID {VehiculoId}.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al eliminar el vehículo con ID {VehiculoId}.", id);
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}
