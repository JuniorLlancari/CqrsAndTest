using CQRS.Domain.Entities.Alumnos;

namespace CQRS.Persistence.Repositories
{
    internal sealed class AlumnoRepository : Repository<Alumno>, IAlumnoRepository
    {
        public AlumnoRepository(CQRSDbContext dbContext) : base(dbContext)
        {
                
        }
    }
}
