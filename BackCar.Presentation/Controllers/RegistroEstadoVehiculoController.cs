using BackCar.Application.Interfaces;
using BackCar._Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackCar.Application.DTOs;

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
            var registros = await _service.ObtenerTodosAsync();
            return Ok(registros);
        }

        [HttpPost]
        public async Task<ActionResult<RegistroEstadoVehiculo>> Create([FromBody] RegistroEstadoVehiculoDto registroDto)
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
            return CreatedAtAction(nameof(GetAll), new { id = nuevoRegistro.Id_RegistroEstadoVehiculo }, nuevoRegistro);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RegistroEstadoVehiculoDto registroDto)
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
            return actualizado == null ? NotFound() : Ok(actualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _service.EliminarAsync(id);
            return eliminado ? NoContent() : NotFound();
        }
    }
}
