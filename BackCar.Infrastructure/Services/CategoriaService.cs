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
            try
            {
                Log.Information("Obteniendo todas las categorías.");
                var categorias = await _context.Categorias.ToListAsync();
                Log.Information("Se obtuvieron {Count} categorías.", categorias.Count);
                return categorias;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener las categorías.");
                throw;
            }
        }

        // Método CREATE: Crear una nueva categoría
        public async Task<Categoria> CrearCategoriaAsync(Categoria categoria)
        {
            try
            {
                Log.Information("Creando una nueva categoría: {Nombre}.", categoria.Nombre);
                await _context.Categorias.AddAsync(categoria);
                await _context.SaveChangesAsync();
                Log.Information("Categoría creada exitosamente con ID: {Id}.", categoria.Id_Categoria);
                return categoria;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al crear la categoría: {Nombre}.", categoria.Nombre);
                throw;
            }
        }

        // Método UPDATE: Actualizar una categoría existente
        public async Task<Categoria> UpdateCategoriaAsync(int id, Categoria categoria)
        {
            try
            {
                Log.Information("Actualizando la categoría con ID: {Id}.", id);
                var categoriaExistente = await _context.Categorias.FindAsync(id);

                if (categoriaExistente == null)
                {
                    Log.Warning("No se encontró la categoría con ID: {Id}.", id);
                    return null;
                }

                categoriaExistente.Nombre = categoria.Nombre;
                await _context.SaveChangesAsync();

                Log.Information("Categoría con ID: {Id} actualizada exitosamente.", id);
                return categoriaExistente;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al actualizar la categoría con ID: {Id}.", id);
                throw;
            }
        }

        // Método DELETE: Eliminar una categoría
        public async Task<bool> DeleteCategoriaAsync(int id)
        {
            try
            {
                Log.Information("Eliminando la categoría con ID: {Id}.", id);
                var categoria = await _context.Categorias.FindAsync(id);

                if (categoria == null)
                {
                    Log.Warning("No se encontró la categoría con ID: {Id}.", id);
                    return false;
                }

                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
                Log.Information("Categoría con ID: {Id} eliminada exitosamente.", id);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al eliminar la categoría con ID: {Id}.", id);
                throw;
            }
        }

        // Método GET: Obtener una categoría por ID
        public async Task<Categoria> ObtenerCategoriaPorIdAsync(int id)
        {
            return await _context.Categorias.FindAsync(id);
        }
    }
}
