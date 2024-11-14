using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackCar._Domain.Entities
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int Id_Usuario {  get; set; }
        public string Nombre {  get; set; }
        public string Correo    { get; set; }
        public string Contrasenia { get; set; }
        public string Telefono  { get; set; }
        public DateTime FechaRegistro { get; set; }
        // Definición de la FK y propiedad de navegación
        [ForeignKey("Rol")]
        public int Roles_Usuarios_id { get; set; }

        public Rol Rol { get; set; } // Propiedad de navegación

    }
}
