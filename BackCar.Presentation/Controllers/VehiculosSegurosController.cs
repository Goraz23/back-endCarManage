using BackCar.Application.DTOs;
using BackCar.Application.Interfaces;
using BackCar.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;


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
        var vehiculosSeguros = await _vehiculosSegurosService.ObtenerVehiculosSegurosAsync();
        return Ok(vehiculosSeguros);
    }

    [HttpPost]
    public async Task<ActionResult> PostVehiculoSeguro([FromBody] VehiculosSegurosDTO vehiculosSegurosDto)
    {
        if (vehiculosSegurosDto == null)
            return BadRequest("El objeto enviado no puede ser nulo.");

        await _vehiculosSegurosService.CrearVehiculoSeguroAsync(vehiculosSegurosDto);

        return Ok("Vehículo-Seguro creado exitosamente.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarVehiculoSeguro(int id, [FromBody] VehiculosSegurosDTO vehiculoSeguroDto)
    {
        try
        {
            // Validar si el DTO es válido
            if (vehiculoSeguroDto == null)
            {
                return BadRequest("El cuerpo de la solicitud no puede ser nulo.");
            }

            // Llamar al servicio para actualizar el registro
            var actualizado = await _vehiculosSegurosService.ActualizarVehiculoSeguroAsync(id, vehiculoSeguroDto);

            // Validar el resultado del servicio
            if (!actualizado)
            {
                return NotFound($"No se encontró el registro con el ID: {id}.");
            }

            return Ok("El registro se actualizó correctamente.");
        }
        catch (Exception ex)
        {
            // Manejo genérico de errores
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var eliminado = await _vehiculosSegurosService.EliminarVehiculoSeguroAsync(id);
        if (!eliminado)
            return NotFound("Vínculo no encontrado");

        return NoContent();
    }
}
