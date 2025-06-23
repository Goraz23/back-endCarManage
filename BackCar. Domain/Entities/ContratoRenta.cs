using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackCar._Domain.Entities
{
    [Table("ContratosRenta")]
    public class ContratoRenta
    {
        [Key]
        public int Id_ContratoRenta { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime FechaDevolucion { get; set; }
        public decimal CostoSubTotal { get; set; }
        public decimal CostoTotal { get; set; }
        public DateTime FechaCreacion { get; set; }

        // Relaciones
        [ForeignKey("Cliente")]
        public int Clientes_id { get; set; }
        public Cliente Cliente { get; set; }

        [ForeignKey("Vehiculo")]
        public int Vehiculos_id { get; set; }
        public Vehiculo Vehiculo { get; set; }
    }
}
