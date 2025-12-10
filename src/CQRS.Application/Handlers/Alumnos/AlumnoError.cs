using CQRS.Domain.Abstraccions;

namespace CQRS.Application.Handlers.Alumnos
{
    public class AlumnoError
    {
        public static Error NoEncontrado = new(
            "Alumno.NoEncontrado",
            "El usuario no se encuentra");
    }
}
