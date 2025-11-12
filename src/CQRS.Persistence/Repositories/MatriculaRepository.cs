using CQRS.Domain.Matriculas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Persistence.Repositories
{
    internal class MatriculaRepository : Repository<Matricula>,IMatriculaRepository
    {
        public MatriculaRepository(CQRSDbContext context):base(context) { }
        
                
        
    }
}
