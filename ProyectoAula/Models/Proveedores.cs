using System.ComponentModel.DataAnnotations;

namespace ProyectoAula.Models
{
    public class Proveedores
    {
        [Key]
        public int Proveedores_ID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Razón Social")]
        public string RazonSocial { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Required]
        [StringLength(50)]
        public string Correo { get; set; }
    }
}
