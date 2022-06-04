using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoAula.Models
{
    public class DetalleVenta
    {
        [Key]
        public int DetalleVenta_ID { get; set; }

        [Required]
        public DateTime FechaVenta { get; set; }

        [Required]
        public int Producto_ID { get; set; }

        [Required]
        public float PrecioVenta { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public float Total { get; set; }

        [ForeignKey("Producto_ID")]
        public Productos Productos { get; set; }
    }
}