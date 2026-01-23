using CQRS.Domain.Abstraccions;
using CQRS.Domain.Entities.Alumnos.Events;
using CQRS.Domain.Entities.Matriculas;
using System.Collections.Generic;

namespace CQRS.Domain.Entities.Alumnos
{
    public class Alumno : Entity
    {
        public Alumno() { }
        private Alumno(Guid alumnoId, string nombreAlumno, AlumnoEstado estado) :base(alumnoId)
        {
            NombreAlumno = nombreAlumno;
            Estado = estado;
        }

        public static Alumno Create(string nombreAlumno)
        {
            var alumno = new Alumno(Guid.NewGuid(), nombreAlumno,AlumnoEstado.Activo);
            alumno.RaiseDomainEvent(new AlumnoCreadoEvent(alumno.Id));

            return alumno;
        }


        public string NombreAlumno { get; private set; } = string.Empty;
        public ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
        public AlumnoEstado Estado { get; set; }
    }
}
