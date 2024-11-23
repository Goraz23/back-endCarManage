using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackCar.Application.DTOs
{
    public class VehiculoUsuarioDto
    {
        public int Id_Vehiculo { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }

        public decimal CostoTemporadaAlta { get; set; }

        public decimal CostoTemporadaBaja { get; set; }
        public int? Usuarios_id { get; set; }

        public bool IsAutomatico { get; set; }
    }
}
