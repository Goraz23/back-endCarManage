using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackCar._Domain.Entities
{
    [Table("Mantenimientos")]
    public class Mantenimiento
    {
        [Key]
        public int Id_Mantenimiento { get; set; }

        public DateTime Fecha { get; set; }
        public string Tipo { get; set; }
        public decimal Costo { get; set; }
        public string Detalles { get; set; }

        // Relaciones
        [ForeignKey("Vehiculo")]
        public int Vehiculo_id { get; set; }
        public Vehiculo Vehiculo { get; set; }
    }
}
