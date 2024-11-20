using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackCar.Application.DTOs
{
    public class VehiculoDetallesDto
    {
        public int Id_Vehiculo { get; set; }

        public string? Imagen { get; set; }
        public bool IsAutomatico { get; set; }
        public int? Usuarios_id { get; set; }
        public int Anio { get; set; }
        public string Placa { get; set; }
        public DateTime FechaUltimoMantenimiento { get; set; }
        public DateTime FechaRegistro { get; set; }

        public bool IsRentado { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public decimal CostoTemporadaAlta { get; set; }

        public decimal CostoTemporadaBaja { get; set; }

        public int Kilometraje { get; set; }

        public string Descripcion { get; set; }

        public int? Categoria_Id { get; set; }
    }
}