using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackCar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedDataTry : Migration
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
                        name: "FK_ContratosRenta_Clientes_Clientes_id",
                        column: x => x.Clientes_id,
                        principalTable: "Clientes",
                        principalColumn: "Id_Cliente",
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

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id_Categoria", "Nombre" },
                values: new object[,]
                {
                    { 1, "SUV" },
                    { 2, "Sedán" },
                    { 3, "Camioneta" },
                    { 4, "Deportivo" }
                });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id_Cliente", "Correo", "NombreCliente", "Telefono" },
                values: new object[,]
                {
                    { 1, "cliente1@gmail.com", "Juan Perez", "1234567890" },
                    { 2, "cliente2@gmail.com", "Diego Aleman", "1234567890" }
                });

            migrationBuilder.InsertData(
                table: "Roles_Usuario",
                columns: new[] { "Id_Rol", "Nombre" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Socio" },
                    { 3, "Cliente" }
                });

            migrationBuilder.InsertData(
                table: "Seguros",
                columns: new[] { "Id_Seguro", "AutorContratado", "FechaInicio", "FechaVencimiento", "Monto", "TipoSeguro" },
                values: new object[,]
                {
                    { 1, "Autor 1", new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 50.00m, "Seguro Básico" },
                    { 2, "Autor 2", new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 100.00m, "Seguro Completo" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id_Usuario", "Contrasenia", "Correo", "FechaRegistro", "Nombre", "Roles_Usuarios_id", "Telefono" },
                values: new object[,]
                {
                    { 1, "$2a$11$XYUho6pOYcNENkWQvKZ59e9iQG2LSjGUxUJZ5PKGkRwAxr8ZL1QDy", "admin@carmanage.com", new DateTime(2025, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin Principal", 1, "5551234567" },
                    { 2, "$2a$11$5aFkFhJkOqgCdeu9k1qMEeSnqz/pDe3PRLGzB3bl2GK9/HcZjCZ9e", "usuario@carmanage.com", new DateTime(2025, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Usuario General", 2, "5559876543" }
                });

            migrationBuilder.InsertData(
                table: "Vehiculos",
                columns: new[] { "Id_Vehiculo", "Anio", "Categoria_Id", "CostoTemporadaAlta", "CostoTemporadaBaja", "Descripcion", "FechaRegistro", "Imagen", "IsAutomatico", "IsMantenimiento", "IsRentado", "Kilometraje", "Marca", "Modelo", "Placa", "Usuarios_id" },
                values: new object[,]
                {
                    { 1, 2020, 1, 1500.00m, 1000.00m, "SUV familiar", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, false, 10000, "Toyota", "RAV4", "ABC123", 1 },
                    { 2, 2019, 2, 1200.00m, 900.00m, "Sedán compacto", new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, false, 20000, "Honda", "Civic", "XYZ456", 2 }
                });

            migrationBuilder.InsertData(
                table: "ContratosRenta",
                columns: new[] { "Id_ContratoRenta", "Clientes_id", "CostoSubTotal", "CostoTotal", "FechaCreacion", "FechaDevolucion", "FechaInicio", "Vehiculos_id" },
                values: new object[,]
                {
                    { 1, 1, 300.00m, 350.00m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 2, 180.00m, 200.00m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 }
                });

            migrationBuilder.InsertData(
                table: "Mantenimientos",
                columns: new[] { "Id_Mantenimiento", "Costo", "Detalles", "Fecha", "Tipo", "Vehiculo_id" },
                values: new object[,]
                {
                    { 1, 50.00m, "Cambio de aceite", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "mecánico", 1 },
                    { 2, 75.00m, "Revisión de frenos", new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "mecánico", 2 }
                });

            migrationBuilder.InsertData(
                table: "Vehiculos_Seguros",
                columns: new[] { "Id_VehiculoSeguro", "Seguros_id", "Vehiculos_id" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Incidentes",
                columns: new[] { "Id_Incidente", "AplicoSeguro", "Descripcion", "FechaIncidente", "Id_ContratoRenta", "Id_Vehiculo" },
                values: new object[,]
                {
                    { 1, false, "Rasguño en la puerta", new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 2, false, "Vidrio roto y puertas rayadas", new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "RegistroEstadoVehiculo",
                columns: new[] { "Id_RegistroEstadoVehiculo", "AplicanCargos", "DetallesRetorno", "Id_ContratoRenta", "KilometrajeFinal", "KilometrajeInicial" },
                values: new object[,]
                {
                    { 1, false, "Todo en orden", 1, 10500, 10000 },
                    { 2, true, "Kilometraje menor al esperado", 2, 19800, 20000 }
                });

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
                name: "Clientes");

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
