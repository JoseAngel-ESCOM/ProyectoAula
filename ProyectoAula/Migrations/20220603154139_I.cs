using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoAula.Migrations
{
    public partial class I : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Producto_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Producto_Nombre = table.Column<string>(maxLength: 50, nullable: false),
                    Marca = table.Column<string>(maxLength: 50, nullable: false),
                    Division = table.Column<string>(maxLength: 30, nullable: false),
                    Categoria = table.Column<string>(maxLength: 30, nullable: false),
                    Color = table.Column<string>(maxLength: 30, nullable: false),
                    Talla = table.Column<float>(nullable: false),
                    Stock = table.Column<int>(nullable: false),
                    Precio_Compra = table.Column<float>(nullable: false),
                    Precio_Venta = table.Column<float>(nullable: false),
                    Total = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Producto_ID);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    Proveedores_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RazonSocial = table.Column<string>(maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(maxLength: 10, nullable: false),
                    Correo = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.Proveedores_ID);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Usuario_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false),
                    Correo = table.Column<string>(maxLength: 50, nullable: false),
                    Clave = table.Column<string>(maxLength: 256, nullable: false),
                    Roles = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Usuario_ID);
                });

            migrationBuilder.CreateTable(
                name: "DetalleVentas",
                columns: table => new
                {
                    DetalleVenta_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaVenta = table.Column<DateTime>(nullable: false),
                    Producto_ID = table.Column<int>(nullable: false),
                    PrecioVenta = table.Column<float>(nullable: false),
                    Cantidad = table.Column<int>(nullable: false),
                    Total = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleVentas", x => x.DetalleVenta_ID);
                    table.ForeignKey(
                        name: "FK_DetalleVentas_Productos_Producto_ID",
                        column: x => x.Producto_ID,
                        principalTable: "Productos",
                        principalColumn: "Producto_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleCompra",
                columns: table => new
                {
                    DetalleCompra_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaCompra = table.Column<DateTime>(nullable: false),
                    Proveedores_ID = table.Column<int>(nullable: false),
                    Producto_ID = table.Column<int>(nullable: false),
                    PrecioCompra = table.Column<float>(nullable: false),
                    PrecioVenta = table.Column<float>(nullable: false),
                    Cantidad = table.Column<int>(nullable: false),
                    Total = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleCompra", x => x.DetalleCompra_ID);
                    table.ForeignKey(
                        name: "FK_DetalleCompra_Productos_Producto_ID",
                        column: x => x.Producto_ID,
                        principalTable: "Productos",
                        principalColumn: "Producto_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleCompra_Proveedores_Proveedores_ID",
                        column: x => x.Proveedores_ID,
                        principalTable: "Proveedores",
                        principalColumn: "Proveedores_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCompra_Producto_ID",
                table: "DetalleCompra",
                column: "Producto_ID");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCompra_Proveedores_ID",
                table: "DetalleCompra",
                column: "Proveedores_ID");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentas_Producto_ID",
                table: "DetalleVentas",
                column: "Producto_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleCompra");

            migrationBuilder.DropTable(
                name: "DetalleVentas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Productos");
        }
    }
}
