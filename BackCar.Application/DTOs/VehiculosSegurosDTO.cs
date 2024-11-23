using BackCar._Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackCar.Application.DTOs
{
    public class VehiculosSegurosDTO
    {        
        // Relaciones
        public int Seguros_id { get; set; }

        public int Vehiculos_id { get; set; }
    }
}
