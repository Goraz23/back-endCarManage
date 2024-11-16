using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackCar.Application.DTOs
{
    public class MantenimientoDTO
    {
        public int Id_Mantenimiento { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; }
        public decimal Costo { get; set; }
        public string Detalles { get; set; }

        // Relación
        public int Vehiculo_id { get; set; }  // Solo ID, no objeto completo
    }
}
