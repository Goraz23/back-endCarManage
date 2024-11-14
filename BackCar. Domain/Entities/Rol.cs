using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackCar._Domain.Entities
{
    [Table("Roles_Usuario")]
    public class Rol
    {
        [Key]
        public int Id_Rol { get; set; }
        public string Nombre { get; set; }
    }
}
