using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppForSEII2526.API.Migrations
{
    /// <inheritdoc />
    public partial class AddProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apellido_1Cliente",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "Apellido_2Cliente",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "NombreCliente",
                table: "Compras");

            migrationBuilder.RenameColumn(
                name: "Puntiación",
                table: "ResenyaBocadillos",
                newName: "Puntuacion");

            migrationBuilder.AddColumn<int>(
                name: "valoracion_General",
                table: "Resenyas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Compras",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "apellido2",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "TipoProductos",
                columns: table => new
                {
                    TipoProductoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoProductos", x => x.TipoProductoId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compras_UserId",
                table: "Compras",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_AspNetUsers_UserId",
                table: "Compras",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_AspNetUsers_UserId",
                table: "Compras");

            migrationBuilder.DropTable(
                name: "TipoProductos");

            migrationBuilder.DropIndex(
                name: "IX_Compras_UserId",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "valoracion_General",
                table: "Resenyas");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Compras");

            migrationBuilder.RenameColumn(
                name: "Puntuacion",
                table: "ResenyaBocadillos",
                newName: "Puntiación");

            migrationBuilder.AddColumn<string>(
                name: "Apellido_1Cliente",
                table: "Compras",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Apellido_2Cliente",
                table: "Compras",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NombreCliente",
                table: "Compras",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "apellido2",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
