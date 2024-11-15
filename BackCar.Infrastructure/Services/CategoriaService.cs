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

    public class CategoriaService : ICategoriaService
    {
        private readonly ApplicationDbContext _context;

        public CategoriaService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Método READ: Obtener todas las categorías
        public async Task<List<Categoria>> ObtenerTodasLasCategoriasAsync()
        {
            return await _context.Categorias.ToListAsync();
        }

        // Método CREATE: Crear una nueva categoría
        public async Task<Categoria> CrearCategoriaAsync(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        // Método UPDATE: Actualizar una categoría existente
        public async Task<Categoria> UpdateCategoriaAsync(int id, Categoria categoria)
        {
            var categoriaExistente = await _context.Categorias.FindAsync(id);

            if (categoriaExistente == null)
            {
                return null; // No se encontró la categoría
            }

            categoriaExistente.Nombre = categoria.Nombre;

            await _context.SaveChangesAsync();
            return categoriaExistente;
        }

        // Método DELETE: Eliminar una categoría
        public async Task<bool> DeleteCategoriaAsync(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return false; // No se encontró la categoría
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
