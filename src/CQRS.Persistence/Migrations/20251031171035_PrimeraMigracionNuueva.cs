using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQRS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PrimeraMigracionNuueva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alumnos",
                columns: table => new
                {
                    AlumnoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombreAlumno = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alumnos", x => x.AlumnoId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alumnos");
        }
    }
}
