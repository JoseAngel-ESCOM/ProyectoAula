using Microsoft.EntityFrameworkCore;

namespace ProyectoAula.Models
{
    public class ContextoBD : DbContext
    {
        public ContextoBD(DbContextOptions<ContextoBD> opt) : base(opt) { }

        public DbSet<Proveedores> Proveedores { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<DetalleCompra> DetalleCompra { get; set; }
        public DbSet<DetalleVenta> DetalleVentas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
