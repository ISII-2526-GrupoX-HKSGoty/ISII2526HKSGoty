using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppForSEII2526.API.Migrations
{
    /// <inheritdoc />
    public partial class CreateIdentitySchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    apellido1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    apellido2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Compras",
                columns: table => new
                {
                    CompraId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Apellido_1Cliente = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Apellido_2Cliente = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FechaCompra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    nBocadillos = table.Column<int>(type: "int", nullable: false),
                    NombreCliente = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Metodo_Pago = table.Column<int>(type: "int", nullable: false),
                    PrecioTotal = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compras", x => x.CompraId);
                });

            migrationBuilder.CreateTable(
                name: "Resenyas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fechaPublicacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    nombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    titulo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resenyas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoBocadillo",
                columns: table => new
                {
                    idTipo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreTipo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoBocadillo", x => x.idTipo);
                });

            migrationBuilder.CreateTable(
                name: "tipoPan",
                columns: table => new
                {
                    PanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipoPan", x => x.PanId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompraBono",
                columns: table => new
                {
                    CompraBonoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    nBonos = table.Column<int>(type: "int", nullable: false),
                    PrecioTotalBono = table.Column<double>(type: "float", nullable: false),
                    metodoPago = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraBono", x => x.CompraBonoId);
                    table.ForeignKey(
                        name: "FK_CompraBono_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BonoBocadillos",
                columns: table => new
                {
                    BonoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cantidadDisponible = table.Column<int>(type: "int", nullable: false),
                    nBocadillos = table.Column<int>(type: "int", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PVP = table.Column<double>(type: "float", nullable: false),
                    TipoBocadilloidTipo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonoBocadillos", x => x.BonoId);
                    table.ForeignKey(
                        name: "FK_BonoBocadillos_TipoBocadillo_TipoBocadilloidTipo",
                        column: x => x.TipoBocadilloidTipo,
                        principalTable: "TipoBocadillo",
                        principalColumn: "idTipo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bocadillos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PVP = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    stock = table.Column<int>(type: "int", nullable: false),
                    tipoPanPanId = table.Column<int>(type: "int", nullable: false),
                    tamaño = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bocadillos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bocadillos_tipoPan_tipoPanPanId",
                        column: x => x.tipoPanPanId,
                        principalTable: "tipoPan",
                        principalColumn: "PanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BonosComprados",
                columns: table => new
                {
                    BonoId = table.Column<int>(type: "int", nullable: false),
                    CompraBonoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonosComprados", x => new { x.BonoId, x.CompraBonoId });
                    table.ForeignKey(
                        name: "FK_BonosComprados_BonoBocadillos_BonoId",
                        column: x => x.BonoId,
                        principalTable: "BonoBocadillos",
                        principalColumn: "BonoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BonosComprados_CompraBono_CompraBonoId",
                        column: x => x.CompraBonoId,
                        principalTable: "CompraBono",
                        principalColumn: "CompraBonoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComprarBocadillos",
                columns: table => new
                {
                    BocadilloId = table.Column<int>(type: "int", nullable: false),
                    CompraId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    NombreBocadillo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComprarBocadillos", x => new { x.BocadilloId, x.CompraId });
                    table.ForeignKey(
                        name: "FK_ComprarBocadillos_Bocadillos_BocadilloId",
                        column: x => x.BocadilloId,
                        principalTable: "Bocadillos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComprarBocadillos_Compras_CompraId",
                        column: x => x.CompraId,
                        principalTable: "Compras",
                        principalColumn: "CompraId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResenyaBocadillos",
                columns: table => new
                {
                    BocadilloId = table.Column<int>(type: "int", nullable: false),
                    ResenyaId = table.Column<int>(type: "int", nullable: false),
                    Puntiación = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResenyaBocadillos", x => new { x.BocadilloId, x.ResenyaId });
                    table.ForeignKey(
                        name: "FK_ResenyaBocadillos_Bocadillos_BocadilloId",
                        column: x => x.BocadilloId,
                        principalTable: "Bocadillos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResenyaBocadillos_Resenyas_ResenyaId",
                        column: x => x.ResenyaId,
                        principalTable: "Resenyas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bocadillos_tipoPanPanId",
                table: "Bocadillos",
                column: "tipoPanPanId");

            migrationBuilder.CreateIndex(
                name: "IX_BonoBocadillos_TipoBocadilloidTipo",
                table: "BonoBocadillos",
                column: "TipoBocadilloidTipo");

            migrationBuilder.CreateIndex(
                name: "IX_BonosComprados_CompraBonoId",
                table: "BonosComprados",
                column: "CompraBonoId");

            migrationBuilder.CreateIndex(
                name: "IX_CompraBono_UserId",
                table: "CompraBono",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ComprarBocadillos_CompraId",
                table: "ComprarBocadillos",
                column: "CompraId");

            migrationBuilder.CreateIndex(
                name: "IX_ResenyaBocadillos_ResenyaId",
                table: "ResenyaBocadillos",
                column: "ResenyaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BonosComprados");

            migrationBuilder.DropTable(
                name: "ComprarBocadillos");

            migrationBuilder.DropTable(
                name: "ResenyaBocadillos");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "BonoBocadillos");

            migrationBuilder.DropTable(
                name: "CompraBono");

            migrationBuilder.DropTable(
                name: "Compras");

            migrationBuilder.DropTable(
                name: "Bocadillos");

            migrationBuilder.DropTable(
                name: "Resenyas");

            migrationBuilder.DropTable(
                name: "TipoBocadillo");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "tipoPan");
        }
    }
}
