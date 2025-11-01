using CQRS.Domain;
using CQRS.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Application.Matriculas
{
    public class CreateMatriculaCommand
    {
        public class CreateMatriculaCommandRequest : IRequest<Unit> 
        {
            public Guid MatriculaId { get; set; }
            public Guid CursoId { get; set; }
            public Guid AlumnoId { get; set; }

        }




        public class CreateMatriculaCommandHanlder : IRequestHandler<CreateMatriculaCommandRequest, Unit>
        {
            private readonly CQRSDbContext _context;
            public CreateMatriculaCommandHanlder(CQRSDbContext context)
            {
                _context = context;
            }

            public  async Task<Unit> Handle(CreateMatriculaCommandRequest request, CancellationToken cancellationToken)
            {

                var alumnoRegistrado= await _context.Matriculas
                    .FirstOrDefaultAsync(cu=>cu.CursoId == request.CursoId && cu.MatriculaId == request.MatriculaId);

                if (alumnoRegistrado != null)
                {
                    throw new Exception("Error El alumno ya tiene el curso matriculado");
                }


                var matricula = new Matricula()
                {
                     MatriculaId=request.MatriculaId,
                     AlumnoId = request.AlumnoId,
                     CursoId = request.CursoId,
                     FechaMatricula = DateTime.Now
                };

                _context.Add(matricula);
                var respuesta = await _context.SaveChangesAsync();
                if (respuesta > 0)
                {
                    return Unit.Value;
                }

                throw new NotImplementedException();
            }
        }



    }
}
