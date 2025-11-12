using CQRS.Domain.Abstraccions;
using CQRS.Domain.Alumnos.Events;
using CQRS.Domain.Matriculas;
using System.Collections.Generic;

namespace CQRS.Domain.Alumnos
{
    public class Alumno : Entity
    {
        private Alumno() { }
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


        public string NombreAlumno { get; set; }
        public ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
        public AlumnoEstado Estado { get; set; }
    }
}
