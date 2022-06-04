using System.ComponentModel.DataAnnotations;

namespace ProyectoAula.Models
{
    public class Usuario
    {
        [Key]
        public int Usuario_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string Correo { get; set; }

        [Required, MaxLength(256)]
        [Display(Name = "Contraseña")]
        public string Clave { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Rol")]
        public string Roles { get; set; }
    }
}
