using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackCar._Domain.Entities
{
    [Table("RegistroEstadoVehiculo")]
    public class RegistroEstadoVehiculo
    {
        [Key]
        public int Id_RegistroEstadoVehiculo { get; set; }

        public int KilometrajeInicial { get; set; }
        public int KilometrajeFinal { get; set; }
        public string DetallesRetorno { get; set; }
        public bool AplicanCargos { get; set; }

        // Relaciones
        [ForeignKey("ContratoRenta")]
        public int Id_ContratoRenta { get; set; }
        public ContratoRenta ContratoRenta { get; set; }
    }
}
