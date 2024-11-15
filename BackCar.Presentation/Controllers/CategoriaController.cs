using BackCar._Domain.Entities;
using BackCar.Application.Interfaces;
using DevExpress.Xpo;
using Microsoft.AspNetCore.Mvc;
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
            var categorias = await _categoriaService.ObtenerTodasLasCategoriasAsync();
            return Ok(categorias);
        }

        // Método POST: Crear una nueva categoría
        [HttpPost]
        public async Task<ActionResult<Categoria>> CreateCategoria([FromBody] Categoria categoria)
        {
            if (categoria == null || string.IsNullOrEmpty(categoria.Nombre))
            {
                return BadRequest("El nombre de la categoría no puede ser nulo o vacío.");
            }

            var nuevaCategoria = await _categoriaService.CrearCategoriaAsync(categoria);
            return CreatedAtAction(nameof(GetCategorias), new { id = nuevaCategoria.Id_Categoria }, nuevaCategoria);
        }

        // Método PUT: Actualizar una categoría existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoria(int id, [FromBody] Categoria categoria)
        {
            var categoriaActualizada = await _categoriaService.UpdateCategoriaAsync(id, categoria);

            if (categoriaActualizada == null)
            {
                return NotFound();
            }

            return Ok(categoriaActualizada);
        }

        // Método DELETE: Eliminar una categoría
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var eliminado = await _categoriaService.DeleteCategoriaAsync(id);

            if (!eliminado)
            {
                return NotFound();
            }

            return NoContent();
        }
    }

}