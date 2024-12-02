using BackCar._Domain.Entities;
using BackCar.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackCar.Infrastructure.Services
{

    public class ClienteService : IClienteService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ClienteService(ApplicationDbContext context)
        {
            _context = context;
            _logger = Log.ForContext<ClienteService>();
        }

        // Método READ: Obtener todos los clientes
        public async Task<List<Cliente>> ObtenerTodosLosClientesAsync()
        {
            try
            {
                _logger.Information("Obteniendo todos los clientes.");
                return await _context.Clientes.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener todos los clientes.");
                throw;
            }
        }

        // Método READ: Obtener cliente por ID
        public async Task<Cliente> ObtenerClientePorIdAsync(int id)
        {
            try
            {
                _logger.Information("Buscando cliente con ID {Id}.", id);
                var cliente = await _context.Clientes.FindAsync(id);

                if (cliente == null)
                {
                    _logger.Warning("Cliente con ID {Id} no encontrado.", id);
                    return null;
                }

                _logger.Information("Cliente con ID {Id} encontrado: {@Cliente}.", id, cliente);
                return cliente;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener el cliente con ID {Id}.", id);
                throw;
            }
        }


        // Método CREATE: Crear un nuevo cliente
        public async Task<Cliente> CrearClienteAsync(Cliente cliente)
        {
            try
            {
                await _context.Clientes.AddAsync(cliente);
                await _context.SaveChangesAsync();
                _logger.Information("Cliente creado exitosamente: {@Cliente}", cliente);
                return cliente;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al crear el cliente: {@Cliente}", cliente);
                throw;
            }
        }

        // Método UPDATE: Actualizar un cliente existente
        public async Task<Cliente> UpdateClienteAsync(int id, Cliente cliente)
        {
            try
            {
                var clienteExistente = await _context.Clientes.FindAsync(id);

                if (clienteExistente == null)
                {
                    _logger.Warning("Cliente con ID {Id} no encontrado para actualizar.", id);
                    return null;
                }

                clienteExistente.NombreCliente = cliente.NombreCliente;
                clienteExistente.Telefono = cliente.Telefono;
                clienteExistente.Correo = cliente.Correo;

                await _context.SaveChangesAsync();
                _logger.Information("Cliente con ID {Id} actualizado exitosamente: {@Cliente}", id, clienteExistente);
                return clienteExistente;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al actualizar el cliente con ID {Id}: {@Cliente}", id, cliente);
                throw;
            }
        }

        // Método DELETE: Eliminar un cliente
        public async Task<bool> DeleteClienteAsync(int id)
        {
            try
            {
                var cliente = await _context.Clientes.FindAsync(id);

                if (cliente == null)
                {
                    _logger.Warning("Cliente con ID {Id} no encontrado para eliminar.", id);
                    return false;
                }

                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
                _logger.Information("Cliente con ID {Id} eliminado exitosamente.", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al eliminar el cliente con ID {Id}.", id);
                throw;
            }
        }
    }
}
