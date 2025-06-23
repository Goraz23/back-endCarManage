using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackCar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id_Categoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id_Categoria);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id_Cliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreCliente = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefono = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Correo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id_Cliente);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Roles_Usuario",
                columns: table => new
                {
                    Id_Rol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles_Usuario", x => x.Id_Rol);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Seguros",
                columns: table => new
                {
                    Id_Seguro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TipoSeguro = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaInicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AutorContratado = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Monto = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seguros", x => x.Id_Seguro);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id_Usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Correo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Contrasenia = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefono = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaRegistro = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Roles_Usuarios_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id_Usuario);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_Usuario_Roles_Usuarios_id",
                        column: x => x.Roles_Usuarios_id,
                        principalTable: "Roles_Usuario",
                        principalColumn: "Id_Rol",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Vehiculos",
                columns: table => new
                {
                    Id_Vehiculo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Marca = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Modelo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Anio = table.Column<int>(type: "int", nullable: false),
                    Imagen = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Placa = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Kilometraje = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CostoTemporadaAlta = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    CostoTemporadaBaja = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    IsRentado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsMantenimiento = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Usuarios_id = table.Column<int>(type: "int", nullable: true),
                    Categoria_Id = table.Column<int>(type: "int", nullable: true),
                    IsAutomatico = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculos", x => x.Id_Vehiculo);
                    table.ForeignKey(
                        name: "FK_Vehiculos_Categorias_Categoria_Id",
                        column: x => x.Categoria_Id,
                        principalTable: "Categorias",
                        principalColumn: "Id_Categoria");
                    table.ForeignKey(
                        name: "FK_Vehiculos_Usuarios_Usuarios_id",
                        column: x => x.Usuarios_id,
                        principalTable: "Usuarios",
                        principalColumn: "Id_Usuario");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ContratosRenta",
                columns: table => new
                {
                    Id_ContratoRenta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FechaInicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaDevolucion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CostoSubTotal = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    CostoTotal = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Clientes_id = table.Column<int>(type: "int", nullable: false),
                    Vehiculos_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContratosRenta", x => x.Id_ContratoRenta);
                    table.ForeignKey(
                        name: "FK_ContratosRenta_Usuarios_Clientes_id",
                        column: x => x.Clientes_id,
                        principalTable: "Usuarios",
                        principalColumn: "Id_Usuario",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContratosRenta_Vehiculos_Vehiculos_id",
                        column: x => x.Vehiculos_id,
                        principalTable: "Vehiculos",
                        principalColumn: "Id_Vehiculo",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Mantenimientos",
                columns: table => new
                {
                    Id_Mantenimiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Tipo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Costo = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Detalles = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Vehiculo_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mantenimientos", x => x.Id_Mantenimiento);
                    table.ForeignKey(
                        name: "FK_Mantenimientos_Vehiculos_Vehiculo_id",
                        column: x => x.Vehiculo_id,
                        principalTable: "Vehiculos",
                        principalColumn: "Id_Vehiculo",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Vehiculos_Seguros",
                columns: table => new
                {
                    Id_VehiculoSeguro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Seguros_id = table.Column<int>(type: "int", nullable: false),
                    Vehiculos_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculos_Seguros", x => x.Id_VehiculoSeguro);
                    table.ForeignKey(
                        name: "FK_Vehiculos_Seguros_Seguros_Seguros_id",
                        column: x => x.Seguros_id,
                        principalTable: "Seguros",
                        principalColumn: "Id_Seguro",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehiculos_Seguros_Vehiculos_Vehiculos_id",
                        column: x => x.Vehiculos_id,
                        principalTable: "Vehiculos",
                        principalColumn: "Id_Vehiculo",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Incidentes",
                columns: table => new
                {
                    Id_Incidente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FechaIncidente = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AplicoSeguro = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Id_ContratoRenta = table.Column<int>(type: "int", nullable: false),
                    Id_Vehiculo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidentes", x => x.Id_Incidente);
                    table.ForeignKey(
                        name: "FK_Incidentes_ContratosRenta_Id_ContratoRenta",
                        column: x => x.Id_ContratoRenta,
                        principalTable: "ContratosRenta",
                        principalColumn: "Id_ContratoRenta",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Incidentes_Vehiculos_Id_Vehiculo",
                        column: x => x.Id_Vehiculo,
                        principalTable: "Vehiculos",
                        principalColumn: "Id_Vehiculo",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RegistroEstadoVehiculo",
                columns: table => new
                {
                    Id_RegistroEstadoVehiculo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    KilometrajeInicial = table.Column<int>(type: "int", nullable: false),
                    KilometrajeFinal = table.Column<int>(type: "int", nullable: false),
                    DetallesRetorno = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AplicanCargos = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Id_ContratoRenta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroEstadoVehiculo", x => x.Id_RegistroEstadoVehiculo);
                    table.ForeignKey(
                        name: "FK_RegistroEstadoVehiculo_ContratosRenta_Id_ContratoRenta",
                        column: x => x.Id_ContratoRenta,
                        principalTable: "ContratosRenta",
                        principalColumn: "Id_ContratoRenta",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ContratosRenta_Clientes_id",
                table: "ContratosRenta",
                column: "Clientes_id");

            migrationBuilder.CreateIndex(
                name: "IX_ContratosRenta_Vehiculos_id",
                table: "ContratosRenta",
                column: "Vehiculos_id");

            migrationBuilder.CreateIndex(
                name: "IX_Incidentes_Id_ContratoRenta",
                table: "Incidentes",
                column: "Id_ContratoRenta");

            migrationBuilder.CreateIndex(
                name: "IX_Incidentes_Id_Vehiculo",
                table: "Incidentes",
                column: "Id_Vehiculo");

            migrationBuilder.CreateIndex(
                name: "IX_Mantenimientos_Vehiculo_id",
                table: "Mantenimientos",
                column: "Vehiculo_id");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroEstadoVehiculo_Id_ContratoRenta",
                table: "RegistroEstadoVehiculo",
                column: "Id_ContratoRenta");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Roles_Usuarios_id",
                table: "Usuarios",
                column: "Roles_Usuarios_id");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_Categoria_Id",
                table: "Vehiculos",
                column: "Categoria_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_Usuarios_id",
                table: "Vehiculos",
                column: "Usuarios_id");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_Seguros_Seguros_id",
                table: "Vehiculos_Seguros",
                column: "Seguros_id");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_Seguros_Vehiculos_id",
                table: "Vehiculos_Seguros",
                column: "Vehiculos_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Incidentes");

            migrationBuilder.DropTable(
                name: "Mantenimientos");

            migrationBuilder.DropTable(
                name: "RegistroEstadoVehiculo");

            migrationBuilder.DropTable(
                name: "Vehiculos_Seguros");

            migrationBuilder.DropTable(
                name: "ContratosRenta");

            migrationBuilder.DropTable(
                name: "Seguros");

            migrationBuilder.DropTable(
                name: "Vehiculos");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Roles_Usuario");
        }
    }
}
