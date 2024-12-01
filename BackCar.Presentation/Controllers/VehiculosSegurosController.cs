using BackCar.Application.DTOs;
using BackCar.Application.Interfaces;
using BackCar.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;


[ApiController]
[Route("api/[controller]")]
public class VehiculosSegurosController : ControllerBase
{
    private readonly IVehiculosSegurosService _vehiculosSegurosService;

    public VehiculosSegurosController(IVehiculosSegurosService vehiculosSegurosService)
    {
        _vehiculosSegurosService = vehiculosSegurosService;
    }

    [HttpGet]
    public async Task<ActionResult<List<VehiculosSegurosDTO>>> ObtenerVehiculosSegurosAsync()
    {
        try
        {
            Log.Information("Iniciando solicitud para obtener todos los vehículos seguros.");
            var vehiculosSeguros = await _vehiculosSegurosService.ObtenerVehiculosSegurosAsync();
            Log.Information("Solicitud completada para obtener todos los vehículos seguros.");
            return Ok(vehiculosSeguros);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error al obtener los vehículos seguros.");
            return StatusCode(500, "Error interno del servidor.");
        }
    }

    [HttpPost]
    public async Task<ActionResult> PostVehiculoSeguro([FromBody] VehiculosSegurosDTO vehiculosSegurosDto)
    {
        try
        {
            if (vehiculosSegurosDto == null)
            {
                Log.Warning("El objeto de solicitud para crear un vehículo seguro es nulo.");
                return BadRequest("El objeto enviado no puede ser nulo.");
            }

            Log.Information("Iniciando solicitud para crear vínculo de vehículo y seguro.");
            await _vehiculosSegurosService.CrearVehiculoSeguroAsync(vehiculosSegurosDto);
            Log.Information("Vinculación creada exitosamente.");
            return Ok("Vehículo-Seguro creado exitosamente.");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error al crear vínculo entre vehículo y seguro.");
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarVehiculoSeguro(int id, [FromBody] VehiculosSegurosDTO vehiculoSeguroDto)
    {
        try
        {
            if (vehiculoSeguroDto == null)
            {
                Log.Warning("El cuerpo de la solicitud para actualizar el vínculo de vehículo y seguro es nulo.");
                return BadRequest("El cuerpo de la solicitud no puede ser nulo.");
            }

            Log.Information("Iniciando solicitud para actualizar vínculo de vehículo y seguro con ID: {VehiculoSeguroId}.", id);
            var actualizado = await _vehiculosSegurosService.ActualizarVehiculoSeguroAsync(id, vehiculoSeguroDto);
            if (!actualizado)
            {
                Log.Warning("No se encontró el registro con el ID: {VehiculoSeguroId}.", id);
                return NotFound($"No se encontró el registro con el ID: {id}.");
            }

            Log.Information("Vinculación de vehículo y seguro con ID: {VehiculoSeguroId} actualizada exitosamente.", id);
            return Ok("El registro se actualizó correctamente.");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error al actualizar vínculo de vehículo y seguro con ID: {VehiculoSeguroId}.", id);
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            Log.Information("Iniciando solicitud para eliminar vínculo de vehículo y seguro con ID: {VehiculoSeguroId}.", id);
            var eliminado = await _vehiculosSegurosService.EliminarVehiculoSeguroAsync(id);
            if (!eliminado)
            {
                Log.Warning("Vínculo de vehículo y seguro con ID: {VehiculoSeguroId} no encontrado.", id);
                return NotFound("Vínculo no encontrado");
            }

            Log.Information("Vínculo de vehículo y seguro con ID: {VehiculoSeguroId} eliminado exitosamente.", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error al eliminar vínculo de vehículo y seguro con ID: {VehiculoSeguroId}.", id);
            return StatusCode(500, "Error interno del servidor.");
        }
    }
}
