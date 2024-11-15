using BackCar.Application.Interfaces;
using BackCar._Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackCar.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // Método GET: Obtener todos los clientes
        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> GetClientes()
        {
            var clientes = await _clienteService.ObtenerTodosLosClientesAsync();
            return Ok(clientes);
        }

        // Método POST: Crear un nuevo cliente
        [HttpPost]
        public async Task<ActionResult<Cliente>> CreateCliente([FromBody] Cliente cliente)
        {
            if (cliente == null || string.IsNullOrEmpty(cliente.NombreCliente))
            {
                return BadRequest("El nombre del cliente no puede ser nulo o vacío.");
            }

            var nuevoCliente = await _clienteService.CrearClienteAsync(cliente);
            return CreatedAtAction(nameof(GetClientes), new { id = nuevoCliente.Id_Cliente }, nuevoCliente);
        }

        // Método PUT: Actualizar un cliente existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(int id, [FromBody] Cliente cliente)
        {
            var clienteActualizado = await _clienteService.UpdateClienteAsync(id, cliente);

            if (clienteActualizado == null)
            {
                return NotFound();
            }

            return Ok(clienteActualizado);
        }

        // Método DELETE: Eliminar un cliente
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var eliminado = await _clienteService.DeleteClienteAsync(id);

            if (!eliminado)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
