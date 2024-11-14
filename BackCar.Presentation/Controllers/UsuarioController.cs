using BackCar.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackCar._Domain.Entities;
using BackCar.Application.Interfaces;

namespace BackCar.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> GetUsuarios()
        {
            var usuarios = await _usuarioService.ObtenerTodosLosUsuariosAsync();
            return Ok(usuarios);
        }
    }
}
