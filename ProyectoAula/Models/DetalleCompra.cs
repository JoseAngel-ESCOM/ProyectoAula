using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoAula.Models
{
    public class DetalleCompra
    {
        [Key]
        public int DetalleCompra_ID { get; set; }

        [Required]
        public DateTime FechaCompra { get; set; }

        [Required]
        public int Proveedores_ID { get; set; }

        [Required]
        public int Producto_ID { get; set; }

        [Required]
        public float PrecioCompra { get; set; }

        [Required]
        public float PrecioVenta{ get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public float Total { get; set; }

        [ForeignKey("Proveedores_ID")]
        public Proveedores Proveedores { get; set; }

        [ForeignKey("Producto_ID")]
        public Productos Productos { get; set; }
    }
}
