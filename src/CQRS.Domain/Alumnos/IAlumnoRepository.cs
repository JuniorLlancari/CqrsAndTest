using CQRS.Domain.Abstraccions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Alumnos
{
    public interface IAlumnoRepository {

        Task<Alumno?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Alumno?> ObtenerPorIdAsync(Guid id, string relaciones, CancellationToken cancellationToken = default);

        Task AgregarAsync(Alumno entidad);

        Task<ICollection<Alumno>> ListarAsync(
            Expression<Func<Alumno, bool>> predicado, bool tracking = false, string? relaciones = null);


    }
}
