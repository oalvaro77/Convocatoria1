using System.ComponentModel.DataAnnotations;

namespace GestionVehiculos.Models
{
    public class Seguro
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Cobertertura { get; set; }
        [Required]
        public string Proveedor { get; set; }
        [Required]
        public DateTime FechaInicio { get; set; }
        [Required]
        public DateTime FechaVencimiento { get; set; }

        [Required]
        public decimal Costo { get; set; }

        // Llave foranea
        public int? VehiculoId   
            { get; set; }
        public Vehiculo? vehiculo { get; set; } 

    }
}
