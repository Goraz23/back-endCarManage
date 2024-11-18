using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackCar._Domain.Entities
{
    [Table("Clientes")]
    public class Cliente
    {
        [Key]
        public int Id_Cliente { get; set; }
        public string NombreCliente { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
    }
}
