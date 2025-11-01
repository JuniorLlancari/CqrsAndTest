using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Entities
{
    public class Alumno
    {
        public Guid AlumnoId { get; set; }
        public string NombreAlumno { get; set; }
        public ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();

    }
}
