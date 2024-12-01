using BackCar._Domain.Entities;
using BackCar.Application.Interfaces;
using DevExpress.Xpo;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;

namespace BackCar.Presentation.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        // Método GET: Obtener todas las categorías
        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> GetCategorias()
        {
            Log.Information("Solicitud para obtener todas las categorías.");
            try
            {
                var categorias = await _categoriaService.ObtenerTodasLasCategoriasAsync();
                Log.Information("Se retornaron {Count} categorías.", categorias.Count);
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al obtener todas las categorías.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        // Método POST: Crear una nueva categoría
        [HttpPost]
        public async Task<ActionResult<Categoria>> CreateCategoria([FromBody] Categoria categoria)
        {
            Log.Information("Solicitud para crear una nueva categoría.");
            if (categoria == null || string.IsNullOrEmpty(categoria.Nombre))
            {
                Log.Warning("Solicitud de creación fallida: Nombre de categoría nulo o vacío.");
                return BadRequest("El nombre de la categoría no puede ser nulo o vacío.");
            }

            try
            {
                var nuevaCategoria = await _categoriaService.CrearCategoriaAsync(categoria);
                Log.Information("Nueva categoría creada con ID: {Id}.", nuevaCategoria.Id_Categoria);
                return CreatedAtAction(nameof(GetCategorias), new { id = nuevaCategoria.Id_Categoria }, nuevaCategoria);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al crear la categoría.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        // Método PUT: Actualizar una categoría existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoria(int id, [FromBody] Categoria categoria)
        {
            Log.Information("Solicitud para actualizar la categoría con ID: {Id}.", id);
            try
            {
                var categoriaActualizada = await _categoriaService.UpdateCategoriaAsync(id, categoria);

                if (categoriaActualizada == null)
                {
                    Log.Warning("No se encontró la categoría con ID: {Id} para actualizar.", id);
                    return NotFound();
                }

                Log.Information("Categoría con ID: {Id} actualizada exitosamente.", id);
                return Ok(categoriaActualizada);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al actualizar la categoría con ID: {Id}.", id);
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        // Método DELETE: Eliminar una categoría
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            Log.Information("Solicitud para eliminar la categoría con ID: {Id}.", id);
            try
            {
                var eliminado = await _categoriaService.DeleteCategoriaAsync(id);

                if (!eliminado)
                {
                    Log.Warning("No se encontró la categoría con ID: {Id} para eliminar.", id);
                    return NotFound();
                }

                Log.Information("Categoría con ID: {Id} eliminada exitosamente.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al eliminar la categoría con ID: {Id}.", id);
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}