using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CQRS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PrimeraMigracion3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: new Guid("495cb9b5-db06-4d16-b93a-614add3c51f7"));

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: new Guid("ad47140d-f541-4c9a-a6ce-8fbefd08f79f"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cursos",
                columns: new[] { "Id", "Descripcion", "FechaCreacion", "FechaPublicacion", "Precio", "Titulo" },
                values: new object[,]
                {
                    { new Guid("495cb9b5-db06-4d16-b93a-614add3c51f7"), "Curso Java", new DateTime(2025, 11, 11, 18, 16, 42, 853, DateTimeKind.Local).AddTicks(306), new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 82m, "Curso de Java de 0 a experto" },
                    { new Guid("ad47140d-f541-4c9a-a6ce-8fbefd08f79f"), "Curso C#", new DateTime(2025, 11, 11, 18, 16, 42, 850, DateTimeKind.Local).AddTicks(5857), new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 56m, "Curso de c#  de 0 a experto" }
                });
        }
    }
}
