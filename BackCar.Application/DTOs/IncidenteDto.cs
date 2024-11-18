using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackCar.Application.DTOs
{
    public class IncidenteDto
    {
        public DateTime FechaIncidente { get; set; }
        public string Descripcion { get; set; }
        public bool AplicoSeguro { get; set; }
        public int Id_ContratoRenta { get; set; } // Relación con el contrato de renta
    }
}
