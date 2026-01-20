using CQRS.Domain.Abstraccions;
using CQRS.Domain.Entities.Alumnos;
using CQRS.Domain.Entities.Cursos;

namespace CQRS.Domain.Entities.Matriculas;

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


    public DateTime FechaMatricula { get; private set; } = DateTime.UtcNow;
    public Guid AlumnoId { get; private set; }
    public Alumno Alumno { get; private set; } = null!;
    public Guid CursoId { get; private set; }
    public Curso Curso { get; private set; } = null!;
    public string Codigo { get; private set; }

}
