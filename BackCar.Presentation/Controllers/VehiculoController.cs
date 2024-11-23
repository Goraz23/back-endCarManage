using BackCar._Domain.Entities;
using BackCar.Application.DTOs;
using BackCar.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<List<VehiculoDTO>>> GetVehiculos()
        {
            var vehiculos = await _vehiculoService.ObtenerTodosLosVehiculosAsync();
            return Ok(vehiculos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehiculoDTO>> GetVehiculoById(int id)
        {
            var vehiculo = await _vehiculoService.ObtenerVehiculoPorIdAsync(id);
            if (vehiculo == null)
                return NotFound("Vehículo no encontrado");

            return Ok(vehiculo);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] VehiculoDTO nuevoVehiculoDTO)
        {
            if (nuevoVehiculoDTO == null)
                return BadRequest("Datos inválidos");

            await _vehiculoService.CrearVehiculoAsync(nuevoVehiculoDTO);
            return CreatedAtAction(nameof(GetVehiculoById), new { id = nuevoVehiculoDTO.Id_Vehiculo }, nuevoVehiculoDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] VehiculoDTO vehiculoActualizadoDTO)
        {
            if (vehiculoActualizadoDTO == null || vehiculoActualizadoDTO.Id_Vehiculo != id)
                return BadRequest("Datos de vehículo inválidos");

            var actualizado = await _vehiculoService.ActualizarVehiculoAsync(id, vehiculoActualizadoDTO);
            if (!actualizado)
                return NotFound("Vehículo no encontrado");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _vehiculoService.EliminarVehiculoAsync(id);
            if (!eliminado)
                return NotFound("Vehículo no encontrado");

            return NoContent();
        }
        //CONTROLADOR PARA FILTRADO POR USUARIO
        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<List<VehiculoDTO>>> GetVehiculosPorUsuario(int usuarioId)
        {
            var vehiculos = await _vehiculoService.ObtenerVehiculosPorUsuarioAsync(usuarioId);

            if (vehiculos == null || !vehiculos.Any())
                return NotFound("No se encontraron vehículos para este usuario.");

            return Ok(vehiculos);
        }

    }
}
