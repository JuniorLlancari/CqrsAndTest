using CQRS.Domain.Abstraccions;
using System.Linq.Expressions;

namespace CQRS.Domain.Matriculas
{
    public interface IMatriculaRepository
    {
        Task<Matricula?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Matricula?> ObtenerPorIdAsync(Guid id, string relaciones, CancellationToken cancellationToken = default);

        Task AgregarAsync(Matricula entidad);

        Task<ICollection<Matricula>> ListarAsync(
            Expression<Func<Matricula, bool>> predicado, bool tracking = false, string? relaciones = null);


        Task<Matricula?> ObtenerPorFiltro(
            Expression<Func<Matricula, bool>> predicado,
            string relaciones = "",
            CancellationToken cancellationToken = default
        );

    }
}
