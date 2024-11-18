using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackCar.Application.DTOs
{
    public class UsuarioDto
    {
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Contrasenia { get; set; }
        public string Telefono { get; set; }
        public int Roles_Usuarios_id { get; set; } // Relación con el rol
    }
}
