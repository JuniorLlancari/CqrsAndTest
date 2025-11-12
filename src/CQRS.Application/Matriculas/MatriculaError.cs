using CQRS.Domain.Abstraccions;

namespace CQRS.Application.Matriculas
{
    public class MatriculaError
    {
        public static Error NoEncontrado = new(
            "Matricula.NoEncontrado",
            "El curso no se encuentra");

        public static Error TieneMatriculaActiva = new(
            "Matricula.TieneMatriculaActiva",
            "El alumno tiene matricula activa");
    }
}
