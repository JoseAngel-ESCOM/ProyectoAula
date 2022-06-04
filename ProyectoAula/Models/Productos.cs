using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ProyectoAula.Models
{
    public class Productos
    {
        [Key]
        public int Producto_ID { get; set; }

        [Required(ErrorMessage = "Se requiere el nombre del producto")]
        [StringLength(50)]
        [Display(Name = "Nombre")]
        public string Producto_Nombre { get; set; }

        [Required(ErrorMessage = "Se requiere la marca del producto")]
        [StringLength(50)]
        public string Marca { get; set; }

        [Required(ErrorMessage = "Se requiere la div del producto ej: Hombre, Mujer")]
        [StringLength(30)]
        [Display(Name = "División")]
        public string Division { get; set; }


        [Required(ErrorMessage = "Se requiere la categoria del producto")]
        [StringLength(30)]
        [Display(Name = "Categoría")]
        public string Categoria { get; set; }

        [Required(ErrorMessage = "Se requiere el color del producto")]
        [StringLength(30)]
        public string Color { get; set; }

        [Required(ErrorMessage = "Se requiere la talla del producto")]
        public float Talla { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        [Display(Name = "Precio de compra")]
        public float Precio_Compra { get; set; }

        [Required]
        [Display(Name = "Precio de venta")]
        public float Precio_Venta { get; set; }

        [Required]
        public float Total { get; set;}
    }
}
