using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Entities
{
    public class Matricula
    {
        public Guid MatriculaId { get; set; }
        public DateTime FechaMatricula { get; set; } = DateTime.UtcNow;

        public Guid AlumnoId { get; set; }
        public Alumno Alumno { get; set; } = null!;

        public Guid CursoId { get; set; }
        public Curso Curso { get; set; } = null!;


    }
}
