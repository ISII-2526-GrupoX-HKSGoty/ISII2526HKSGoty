using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppForSEII2526.API.Migrations
{
    /// <inheritdoc />
    public partial class Compra_Producto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compra_Productos_Compras_IdCompra",
                table: "Compra_Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Compra_Productos_Productos_IdProducto",
                table: "Compra_Productos");

            migrationBuilder.RenameColumn(
                name: "IdProducto",
                table: "Compra_Productos",
                newName: "ProductoId");

            migrationBuilder.RenameColumn(
                name: "IdCompra",
                table: "Compra_Productos",
                newName: "CompraId");

            migrationBuilder.RenameIndex(
                name: "IX_Compra_Productos_IdProducto",
                table: "Compra_Productos",
                newName: "IX_Compra_Productos_ProductoId");

            migrationBuilder.RenameIndex(
                name: "IX_Compra_Productos_IdCompra",
                table: "Compra_Productos",
                newName: "IX_Compra_Productos_CompraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_Productos_Compras_CompraId",
                table: "Compra_Productos",
                column: "CompraId",
                principalTable: "Compras",
                principalColumn: "CompraId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_Productos_Productos_ProductoId",
                table: "Compra_Productos",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compra_Productos_Compras_CompraId",
                table: "Compra_Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Compra_Productos_Productos_ProductoId",
                table: "Compra_Productos");

            migrationBuilder.RenameColumn(
                name: "ProductoId",
                table: "Compra_Productos",
                newName: "IdProducto");

            migrationBuilder.RenameColumn(
                name: "CompraId",
                table: "Compra_Productos",
                newName: "IdCompra");

            migrationBuilder.RenameIndex(
                name: "IX_Compra_Productos_ProductoId",
                table: "Compra_Productos",
                newName: "IX_Compra_Productos_IdProducto");

            migrationBuilder.RenameIndex(
                name: "IX_Compra_Productos_CompraId",
                table: "Compra_Productos",
                newName: "IX_Compra_Productos_IdCompra");

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_Productos_Compras_IdCompra",
                table: "Compra_Productos",
                column: "IdCompra",
                principalTable: "Compras",
                principalColumn: "CompraId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_Productos_Productos_IdProducto",
                table: "Compra_Productos",
                column: "IdProducto",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
