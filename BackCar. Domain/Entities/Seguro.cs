using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackCar._Domain.Entities
{
    [Table("Seguros")]
    public class Seguro
    {
        [Key]
        public int Id_Seguro { get; set; }

        public string TipoSeguro { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string AutorContratado { get; set; }
        public decimal Monto { get; set; }
    }
}
