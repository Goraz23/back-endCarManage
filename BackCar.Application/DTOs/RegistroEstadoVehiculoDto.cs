using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackCar.Application.DTOs
{
    public class RegistroEstadoVehiculoDto
    {
        public int KilometrajeInicial { get; set; }
        public int KilometrajeFinal { get; set; }
        public string DetallesRetorno { get; set; }
        public bool AplicanCargos { get; set; }
        public int Id_ContratoRenta { get; set; }
    }
}
