using BackCar._Domain.Entities;
using BackCar.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public ClienteService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Método READ: Obtener todos los clientes
        public async Task<List<Cliente>> ObtenerTodosLosClientesAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        // Método CREATE: Crear un nuevo cliente
        public async Task<Cliente> CrearClienteAsync(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        // Método UPDATE: Actualizar un cliente existente
        public async Task<Cliente> UpdateClienteAsync(int id, Cliente cliente)
        {
            var clienteExistente = await _context.Clientes.FindAsync(id);

            if (clienteExistente == null)
            {
                return null; // No se encontró el cliente
            }

            clienteExistente.NombreCliente = cliente.NombreCliente;
            clienteExistente.Telefono = cliente.Telefono;
            clienteExistente.Correo = cliente.Correo;

            await _context.SaveChangesAsync();
            return clienteExistente;
        }

        // Método DELETE: Eliminar un cliente
        public async Task<bool> DeleteClienteAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return false; // No se encontró el cliente
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
