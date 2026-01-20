using System.Linq.Expressions;

namespace CQRS.Domain.Entities.Alumnos
{
    public interface IAlumnoRepository {

        Task<Alumno?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Alumno?> ObtenerPorIdAsync(Guid id, string relaciones, CancellationToken cancellationToken = default);

        Task AgregarAsync(Alumno entidad);

        Task<ICollection<Alumno>> ListarAsync(
            Expression<Func<Alumno, bool>> predicado, bool tracking = false, string? relaciones = null);


    }
}
