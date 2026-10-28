using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGCAE.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Identificacion = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cargo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Area = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PuntosAcceso",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Habilitado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PuntosAcceso", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Credenciales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmpleadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FechaAsignacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credenciales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Credenciales_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegistrosAcceso",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmpleadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PuntoAccesoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Resultado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Motivo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FechaHora = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosAcceso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrosAcceso_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegistrosAcceso_PuntosAcceso_PuntoAccesoId",
                        column: x => x.PuntoAccesoId,
                        principalTable: "PuntosAcceso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Credenciales_Codigo",
                table: "Credenciales",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Credenciales_EmpleadoId_Estado",
                table: "Credenciales",
                columns: new[] { "EmpleadoId", "Estado" });

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_Identificacion",
                table: "Empleados",
                column: "Identificacion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PuntosAcceso_Nombre",
                table: "PuntosAcceso",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosAcceso_EmpleadoId_FechaHora",
                table: "RegistrosAcceso",
                columns: new[] { "EmpleadoId", "FechaHora" });

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosAcceso_FechaHora",
                table: "RegistrosAcceso",
                column: "FechaHora");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosAcceso_PuntoAccesoId",
                table: "RegistrosAcceso",
                column: "PuntoAccesoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Credenciales");

            migrationBuilder.DropTable(
                name: "RegistrosAcceso");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "PuntosAcceso");
        }
    }
}
