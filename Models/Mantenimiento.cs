using System.ComponentModel.DataAnnotations;

namespace GestionVehiculos.Models
{
    public class Mantenimiento
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public String Descripcion { get; set; }

        [Required]
        public decimal Costo { get; set; }
        [Required]
        public string Proveedor { get; set; }

        //Llave foranea
        public int? VehiculoId   { get; set; }
        public Vehiculo? Vehiculo { get; set; }
    }
}
