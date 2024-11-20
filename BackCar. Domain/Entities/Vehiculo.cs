using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackCar._Domain.Entities
{
    [Table("Vehiculos")]
    public class Vehiculo
    {
        [Key]
        public int Id_Vehiculo { get; set; }

        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Anio { get; set; }
        public string? Imagen { get; set; }
        public string Descripcion { get; set; }
        public string Placa { get; set; }
        public int Kilometraje { get; set; }
        public DateTime FechaUltimoMantenimiento { get; set; }
        public DateTime FechaRegistro { get; set; }
        public decimal CostoTemporadaAlta { get; set; }
        public decimal CostoTemporadaBaja { get; set; }
        public bool IsRentado { get; set; }
        public bool IsMantenimiento { get; set; }

        // Relaciones
        [ForeignKey("Usuario")]
        public int? Usuarios_id { get; set; }
        public Usuario Usuario { get; set; }

        [ForeignKey("Categoria")]
        public int? Categoria_Id { get; set; }
        public Categoria Categoria { get; set; }

        // Nuevo Campo
        public bool IsAutomatico { get; set; }

    }
}
