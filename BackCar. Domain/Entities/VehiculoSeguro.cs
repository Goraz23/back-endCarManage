using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackCar._Domain.Entities
{
    [Table("Vehiculos_Seguros")]
    public class VehiculoSeguro
    {
        [Key]
        public int Id_VehiculoSeguro { get; set; }

        // Relaciones
        [ForeignKey("Seguro")]
        public int Seguros_id { get; set; }
        public Seguro Seguro { get; set; }

        [ForeignKey("Vehiculo")]
        public int Vehiculos_id { get; set; }
        public Vehiculo Vehiculo { get; set; }
    }
}
