using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ESCUELAS",
                columns: table => new
                {
                    ID_ESCUELA = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOMBRE_ESCUELA = table.Column<string>(type: "varchar(120)", unicode: false, maxLength: 120, nullable: true),
                    ESTADO = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ESCUELAS__99BC376F185E0BAA", x => x.ID_ESCUELA);
                });

            migrationBuilder.CreateTable(
                name: "ESTADO_PEDIDOS",
                columns: table => new
                {
                    ID_ESTADO_PEDIDO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ESTADO_PEDIDO = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ESTADO_P__164DA606AF643A4C", x => x.ID_ESTADO_PEDIDO);
                });

            migrationBuilder.CreateTable(
                name: "ROLES",
                columns: table => new
                {
                    ID_ROL = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOMBRE_ROL = table.Column<string>(type: "varchar(120)", unicode: false, maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ROLES__203B0F6801E05D87", x => x.ID_ROL);
                });

            migrationBuilder.CreateTable(
                name: "TIPO_CEDULA",
                columns: table => new
                {
                    ID_TIPO_CEDULA = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TIPO_CEDULA = table.Column<string>(type: "varchar(120)", unicode: false, maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TIPO_CED__0CDCFB15550C3FBE", x => x.ID_TIPO_CEDULA);
                });

            migrationBuilder.CreateTable(
                name: "PROVEEDORES",
                columns: table => new
                {
                    ID_PROVEEDOR = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOMBRE_PROVEEDOR = table.Column<string>(type: "varchar(120)", unicode: false, maxLength: 120, nullable: true),
                    TELEFONO = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    CORREO_ELECTRONICO = table.Column<string>(type: "varchar(120)", unicode: false, maxLength: 120, nullable: true),
                    DIRECCION = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    ESTADO = table.Column<bool>(type: "bit", nullable: true),
                    ID_ESCUELA = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PROVEEDO__733DB4C42D0EFDB1", x => x.ID_PROVEEDOR);
                    table.ForeignKey(
                        name: "FK__PROVEEDOR__ID_ES__3F466844",
                        column: x => x.ID_ESCUELA,
                        principalTable: "ESCUELAS",
                        principalColumn: "ID_ESCUELA");
                });

            migrationBuilder.CreateTable(
                name: "USUARIOS",
                columns: table => new
                {
                    ID_USUARIO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOMBRE_COMPLETO = table.Column<string>(type: "varchar(120)", unicode: false, maxLength: 120, nullable: true),
                    ID_TIPO_CEDULA = table.Column<int>(type: "int", nullable: true),
                    CEDULA = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    TELEFONO = table.Column<string>(type: "varchar(120)", unicode: false, maxLength: 120, nullable: true),
                    DIRECCION = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    CORREO_ELECTRONICO = table.Column<string>(type: "varchar(120)", unicode: false, maxLength: 120, nullable: true),
                    CLAVE = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    ESTADO = table.Column<bool>(type: "bit", nullable: true),
                    ID_ESCUELA = table.Column<int>(type: "int", nullable: true),
                    ID_ROL = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__USUARIOS__91136B908550F84D", x => x.ID_USUARIO);
                    table.ForeignKey(
                        name: "FK__USUARIOS__ID_ROL__49C3F6B7",
                        column: x => x.ID_ESCUELA,
                        principalTable: "ESCUELAS",
                        principalColumn: "ID_ESCUELA");
                    table.ForeignKey(
                        name: "FK__USUARIOS__ID_ROL__4AB81AF0",
                        column: x => x.ID_ROL,
                        principalTable: "ROLES",
                        principalColumn: "ID_ROL");
                    table.ForeignKey(
                        name: "FK__USUARIOS__ID_TIP__4BAC3F29",
                        column: x => x.ID_TIPO_CEDULA,
                        principalTable: "TIPO_CEDULA",
                        principalColumn: "ID_TIPO_CEDULA");
                });

            migrationBuilder.CreateTable(
                name: "PRODUCTOS",
                columns: table => new
                {
                    ID_PRODUCTO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOMBRE_PRODUCTO = table.Column<string>(type: "varchar(120)", unicode: false, maxLength: 120, nullable: true),
                    CANTIDAD = table.Column<int>(type: "int", nullable: true),
                    IMAGEN = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ESTADO = table.Column<bool>(type: "bit", nullable: true),
                    ID_PROVEEDOR = table.Column<int>(type: "int", nullable: true),
                    ID_ESCUELA = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PRODUCTO__88BD0357B3FB778C", x => x.ID_PRODUCTO);
                    table.ForeignKey(
                        name: "FK__PRODUCTOS__ID_ES__4316F928",
                        column: x => x.ID_ESCUELA,
                        principalTable: "ESCUELAS",
                        principalColumn: "ID_ESCUELA");
                    table.ForeignKey(
                        name: "FK__PRODUCTOS__ID_PR__4222D4EF",
                        column: x => x.ID_PROVEEDOR,
                        principalTable: "PROVEEDORES",
                        principalColumn: "ID_PROVEEDOR");
                });

            migrationBuilder.CreateTable(
                name: "BITACORA",
                columns: table => new
                {
                    ID_BITACORA = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ACCION = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    FECHA_HORA = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ESTADO = table.Column<bool>(type: "bit", nullable: true),
                    ID_ESCUELA = table.Column<int>(type: "int", nullable: true),
                    ID_USUARIO = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BITACORA__44E70BF34A030070", x => x.ID_BITACORA);
                    table.ForeignKey(
                        name: "FK__BITACORA__ID_ESC__4E88ABD4",
                        column: x => x.ID_ESCUELA,
                        principalTable: "ESCUELAS",
                        principalColumn: "ID_ESCUELA");
                    table.ForeignKey(
                        name: "FK__BITACORA__ID_USU__4F7CD00D",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIOS",
                        principalColumn: "ID_USUARIO");
                });

            migrationBuilder.CreateTable(
                name: "PEDIDOS",
                columns: table => new
                {
                    ID_PEDIDO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_PRODUCTO = table.Column<int>(type: "int", nullable: true),
                    FECHA_HORA_INGRESO = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CANTIDAD = table.Column<int>(type: "int", nullable: true),
                    ID_USUARIO = table.Column<int>(type: "int", nullable: true),
                    ID_ESCUELA = table.Column<int>(type: "int", nullable: true),
                    ID_ESTADO_PEDIDO = table.Column<int>(type: "int", nullable: true),
                    ESTADO = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PEDIDOS__A05C2F2A5D74C150", x => x.ID_PEDIDO);
                    table.ForeignKey(
                        name: "FK__PEDIDOS__ID_ESCU__534D60F1",
                        column: x => x.ID_ESCUELA,
                        principalTable: "ESCUELAS",
                        principalColumn: "ID_ESCUELA");
                    table.ForeignKey(
                        name: "FK__PEDIDOS__ID_ESTA__5441852A",
                        column: x => x.ID_ESTADO_PEDIDO,
                        principalTable: "ESTADO_PEDIDOS",
                        principalColumn: "ID_ESTADO_PEDIDO");
                    table.ForeignKey(
                        name: "FK__PEDIDOS__ID_PROD__5535A963",
                        column: x => x.ID_PRODUCTO,
                        principalTable: "PRODUCTOS",
                        principalColumn: "ID_PRODUCTO");
                    table.ForeignKey(
                        name: "FK__PEDIDOS__ID_USUA__52593CB8",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIOS",
                        principalColumn: "ID_USUARIO");
                });

            migrationBuilder.CreateTable(
                name: "PRODUCTOS_DIA",
                columns: table => new
                {
                    ID_PRODUCTO_DIA = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_PRODUCTO = table.Column<int>(type: "int", nullable: true),
                    CANTIDAD = table.Column<int>(type: "int", nullable: true),
                    FECHA = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ESTADO = table.Column<bool>(type: "bit", nullable: true),
                    ID_ESCUELA = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PRODUCTO__4695E6223F79FED1", x => x.ID_PRODUCTO_DIA);
                    table.ForeignKey(
                        name: "FK__PRODUCTOS__ID_ES__45F365D3",
                        column: x => x.ID_ESCUELA,
                        principalTable: "ESCUELAS",
                        principalColumn: "ID_ESCUELA");
                    table.ForeignKey(
                        name: "FK__PRODUCTOS__ID_PR__46E78A0C",
                        column: x => x.ID_PRODUCTO,
                        principalTable: "PRODUCTOS",
                        principalColumn: "ID_PRODUCTO");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BITACORA_ID_ESCUELA",
                table: "BITACORA",
                column: "ID_ESCUELA");

            migrationBuilder.CreateIndex(
                name: "IX_BITACORA_ID_USUARIO",
                table: "BITACORA",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_PEDIDOS_ID_ESCUELA",
                table: "PEDIDOS",
                column: "ID_ESCUELA");

            migrationBuilder.CreateIndex(
                name: "IX_PEDIDOS_ID_ESTADO_PEDIDO",
                table: "PEDIDOS",
                column: "ID_ESTADO_PEDIDO");

            migrationBuilder.CreateIndex(
                name: "IX_PEDIDOS_ID_PRODUCTO",
                table: "PEDIDOS",
                column: "ID_PRODUCTO");

            migrationBuilder.CreateIndex(
                name: "IX_PEDIDOS_ID_USUARIO",
                table: "PEDIDOS",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTOS_ID_ESCUELA",
                table: "PRODUCTOS",
                column: "ID_ESCUELA");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTOS_ID_PROVEEDOR",
                table: "PRODUCTOS",
                column: "ID_PROVEEDOR");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTOS_DIA_ID_ESCUELA",
                table: "PRODUCTOS_DIA",
                column: "ID_ESCUELA");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTOS_DIA_ID_PRODUCTO",
                table: "PRODUCTOS_DIA",
                column: "ID_PRODUCTO");

            migrationBuilder.CreateIndex(
                name: "IX_PROVEEDORES_ID_ESCUELA",
                table: "PROVEEDORES",
                column: "ID_ESCUELA");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIOS_ID_ESCUELA",
                table: "USUARIOS",
                column: "ID_ESCUELA");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIOS_ID_ROL",
                table: "USUARIOS",
                column: "ID_ROL");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIOS_ID_TIPO_CEDULA",
                table: "USUARIOS",
                column: "ID_TIPO_CEDULA");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BITACORA");

            migrationBuilder.DropTable(
                name: "PEDIDOS");

            migrationBuilder.DropTable(
                name: "PRODUCTOS_DIA");

            migrationBuilder.DropTable(
                name: "ESTADO_PEDIDOS");

            migrationBuilder.DropTable(
                name: "USUARIOS");

            migrationBuilder.DropTable(
                name: "PRODUCTOS");

            migrationBuilder.DropTable(
                name: "ROLES");

            migrationBuilder.DropTable(
                name: "TIPO_CEDULA");

            migrationBuilder.DropTable(
                name: "PROVEEDORES");

            migrationBuilder.DropTable(
                name: "ESCUELAS");
        }
    }
}
