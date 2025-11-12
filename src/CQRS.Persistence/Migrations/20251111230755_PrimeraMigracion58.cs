using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CQRS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PrimeraMigracion58 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alumnos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombreAlumno = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alumnos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaPublicacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Precio = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Matriculas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaMatricula = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AlumnoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CursoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matriculas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matriculas_Alumnos_AlumnoId",
                        column: x => x.AlumnoId,
                        principalTable: "Alumnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matriculas_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cursos",
                columns: new[] { "Id", "Descripcion", "FechaCreacion", "FechaPublicacion", "Precio", "Titulo" },
                values: new object[,]
                {
                    { new Guid("7c8030cd-54a4-4413-be2a-c7f6b44e2b2e"), "Curso Java", new DateTime(2025, 11, 11, 18, 7, 55, 404, DateTimeKind.Local).AddTicks(8675), new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 82m, "Curso de Java de 0 a experto" },
                    { new Guid("891dee39-e8d1-4bf8-be03-105783fe5624"), "Curso C#", new DateTime(2025, 11, 11, 18, 7, 55, 402, DateTimeKind.Local).AddTicks(5650), new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 56m, "Curso de c#  de 0 a experto" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_AlumnoId",
                table: "Matriculas",
                column: "AlumnoId");

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_CursoId",
                table: "Matriculas",
                column: "CursoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matriculas");

            migrationBuilder.DropTable(
                name: "Alumnos");

            migrationBuilder.DropTable(
                name: "Cursos");
        }
    }
}
