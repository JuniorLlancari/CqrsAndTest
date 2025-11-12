using System.Linq.Expressions;

namespace CQRS.Domain.Cursos
{
    public interface ICursoRepository
    {
        Task<Curso?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Curso?> ObtenerPorIdAsync(Guid id, string relaciones, CancellationToken cancellationToken = default);

        Task AgregarAsync(Curso entidad);

        Task<ICollection<Curso>> ListarAsync(
            Expression<Func<Curso, bool>> predicado, bool tracking = false, string? relaciones = null);

    }
}
