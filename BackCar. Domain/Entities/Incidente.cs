﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackCar._Domain.Entities
{
    [Table("Incidentes")]
    public class Incidente
    {
        [Key]
        public int Id_Incidente { get; set; }

        public DateTime FechaIncidente { get; set; }
        public string Descripcion { get; set; }
        public bool AplicoSeguro { get; set; }

        // Relaciones
        [ForeignKey("ContratoRenta")]
        public int Id_ContratoRenta { get; set; }
        public ContratoRenta ContratoRenta { get; set; }
    }
}