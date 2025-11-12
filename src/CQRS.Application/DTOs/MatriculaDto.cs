using CQRS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.DTOs
{
    public class MatriculaDto
    {
        public Guid Id { get; set; }
        public DateTime FechaMatricula { get; set; } = DateTime.UtcNow;

        public Guid AlumnoId { get; set; }
        public string NombreAlumno { get; set; } = null!;

        public Guid CursoId { get; set; }
        public string NombreCurso  { get; set; } = null!;
        public string Codigo { get; set; }

    }
}
