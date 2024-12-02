using BackCar.Application.Interfaces;
using BackCar._Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;
using ILogger = Serilog.ILogger;

namespace BackCar.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly ILogger _logger;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
            _logger = Log.ForContext<ClienteController>();
        }

        // Método GET: Obtener todos los clientes
        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> GetClientes()
        {
            try
            {
                _logger.Information("Petición para obtener todos los clientes.");
                var clientes = await _clienteService.ObtenerTodosLosClientesAsync();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener la lista de clientes.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        // Método GET: Obtener cliente por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetClienteById(int id)
        {
            try
            {
                _logger.Information("Petición para obtener cliente con ID {Id}.", id);
                var cliente = await _clienteService.ObtenerClientePorIdAsync(id);

                if (cliente == null)
                {
                    _logger.Warning("Cliente con ID {Id} no encontrado.", id);
                    return NotFound("Cliente no encontrado.");
                }

                _logger.Information("Cliente con ID {Id} encontrado: {@Cliente}.", id, cliente);
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener cliente con ID {Id}.", id);
                return StatusCode(500, "Error interno del servidor.");
            }
        }


        // Método POST: Crear un nuevo cliente
        [HttpPost]
        public async Task<ActionResult<Cliente>> CreateCliente([FromBody] Cliente cliente)
        {
            if (cliente == null || string.IsNullOrEmpty(cliente.NombreCliente))
            {
                _logger.Warning("Intento de crear un cliente con datos inválidos: {@Cliente}", cliente);
                return BadRequest("El nombre del cliente no puede ser nulo o vacío.");
            }

            try
            {
                var nuevoCliente = await _clienteService.CrearClienteAsync(cliente);
                _logger.Information("Cliente creado exitosamente: {@Cliente}", nuevoCliente);
                return CreatedAtAction(nameof(GetClientes), new { id = nuevoCliente.Id_Cliente }, nuevoCliente);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al crear un cliente: {@Cliente}", cliente);
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        // Método PUT: Actualizar un cliente existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(int id, [FromBody] Cliente cliente)
        {
            try
            {
                var clienteActualizado = await _clienteService.UpdateClienteAsync(id, cliente);

                if (clienteActualizado == null)
                {
                    _logger.Warning("No se encontró el cliente con ID {Id} para actualizar.", id);
                    return NotFound();
                }

                _logger.Information("Cliente con ID {Id} actualizado exitosamente: {@Cliente}", id, clienteActualizado);
                return Ok(clienteActualizado);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al actualizar el cliente con ID {Id}: {@Cliente}", id, cliente);
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        // Método DELETE: Eliminar un cliente
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            try
            {
                var eliminado = await _clienteService.DeleteClienteAsync(id);

                if (!eliminado)
                {
                    _logger.Warning("No se encontró el cliente con ID {Id} para eliminar.", id);
                    return NotFound();
                }

                _logger.Information("Cliente con ID {Id} eliminado exitosamente.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al eliminar el cliente con ID {Id}.", id);
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}
