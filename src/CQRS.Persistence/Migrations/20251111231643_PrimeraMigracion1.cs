using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CQRS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PrimeraMigracion1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: new Guid("7c8030cd-54a4-4413-be2a-c7f6b44e2b2e"));

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: new Guid("891dee39-e8d1-4bf8-be03-105783fe5624"));

            migrationBuilder.InsertData(
                table: "Cursos",
                columns: new[] { "Id", "Descripcion", "FechaCreacion", "FechaPublicacion", "Precio", "Titulo" },
                values: new object[,]
                {
                    { new Guid("495cb9b5-db06-4d16-b93a-614add3c51f7"), "Curso Java", new DateTime(2025, 11, 11, 18, 16, 42, 853, DateTimeKind.Local).AddTicks(306), new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 82m, "Curso de Java de 0 a experto" },
                    { new Guid("ad47140d-f541-4c9a-a6ce-8fbefd08f79f"), "Curso C#", new DateTime(2025, 11, 11, 18, 16, 42, 850, DateTimeKind.Local).AddTicks(5857), new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 56m, "Curso de c#  de 0 a experto" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: new Guid("495cb9b5-db06-4d16-b93a-614add3c51f7"));

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: new Guid("ad47140d-f541-4c9a-a6ce-8fbefd08f79f"));

            migrationBuilder.InsertData(
                table: "Cursos",
                columns: new[] { "Id", "Descripcion", "FechaCreacion", "FechaPublicacion", "Precio", "Titulo" },
                values: new object[,]
                {
                    { new Guid("7c8030cd-54a4-4413-be2a-c7f6b44e2b2e"), "Curso Java", new DateTime(2025, 11, 11, 18, 7, 55, 404, DateTimeKind.Local).AddTicks(8675), new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 82m, "Curso de Java de 0 a experto" },
                    { new Guid("891dee39-e8d1-4bf8-be03-105783fe5624"), "Curso C#", new DateTime(2025, 11, 11, 18, 7, 55, 402, DateTimeKind.Local).AddTicks(5650), new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 56m, "Curso de c#  de 0 a experto" }
                });
        }
    }
}
