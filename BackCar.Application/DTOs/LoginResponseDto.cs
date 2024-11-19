using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackCar.Application.DTOs
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string Nombre { get; set; }
        public string Rol { get; set; }
    }
}
