using CQRS.Domain.Abstraccions;
using CQRS.Domain.Alumnos;
using CQRS.Domain.Cursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CQRS.Domain.Matriculas
{
    public class Matricula : Entity
    {
        public Matricula()
        {
                
        }
        private Matricula(
            Guid matriculaId,
            DateTime fechaMatricula,
            Guid alumnoId,
            Guid cursoId,
            string codigo):base(matriculaId)
        {
            FechaMatricula = fechaMatricula;
            AlumnoId = alumnoId;
            CursoId = cursoId;
            Codigo = codigo;
        }


        public static Matricula Create(
            DateTime fechaMatricula,
            Guid alumnoId,
            Guid cursoId,
            string codigo)
        {
            var matricula = new Matricula(Guid.NewGuid(),fechaMatricula, alumnoId, cursoId, codigo);
            return matricula;
        }


        public DateTime FechaMatricula { get; set; } = DateTime.UtcNow;
        public Guid AlumnoId { get; set; }
        public Alumno Alumno { get; set; } = null!;
        public Guid CursoId { get; set; }
        public Curso Curso { get; set; } = null!;
        public string Codigo { get; set; }

    }
}
