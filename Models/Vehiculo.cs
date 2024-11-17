using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionVehiculos.Models
{
    public class Vehiculo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Marca { get; set; }
        [Required]
        public string Modelo { get; set; }
        [Required]
        public string Placa { get; set; }
        [Required]
        public string Tipo { get; set; }
        [Required]
        public int Anio { get; set; }
        
        public string? Foto { get; set; }

        //Llave foranea
        public int? ID_Cliente { get; set; }
       
        public Cliente? Cliente { get; set; }

        public ICollection<Mantenimiento> Mantenimientos { get; set; } = new List<Mantenimiento>();
        public ICollection<Seguro> Seguros { get; set; } = new List<Seguro>();

       

    
    }
}
