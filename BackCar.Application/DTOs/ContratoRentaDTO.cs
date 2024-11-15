using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackCar.Application.DTOs
{
    public class ContratoRentaDTO
    {
        public int Id_ContratoRenta { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaDevolucion { get; set; }
        public decimal CostoSubTotal { get; set; }
        public decimal CostoTotal { get; set; }
        public DateTime FechaCreacion { get; set; }

        // Relaciones
        public int Clientes_id { get; set; }  // Solo el ID del Cliente
        public int Vehiculos_id { get; set; }  // Solo el ID del Vehiculo
    }
}
