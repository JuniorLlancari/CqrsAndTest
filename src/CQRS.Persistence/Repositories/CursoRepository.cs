using CQRS.Domain.Cursos;

namespace CQRS.Persistence.Repositories
{
    internal class CursoRepository : Repository<Curso>, ICursoRepository
    {
 
        public CursoRepository(CQRSDbContext dbContext):base(dbContext) { }
         
    }
}
