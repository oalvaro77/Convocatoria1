using System.ComponentModel.DataAnnotations;

namespace GestionVehiculos.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Documento { get; set; }

        [Required]
        public int Contacto { get; set; }
        [Required]
        public string Direccion { get; set; }
        public string TipoCliente { get; set; }

        public ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();



    }
}
