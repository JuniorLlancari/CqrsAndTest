using CQRS.Domain.Abstraccions;
using CQRS.Domain.Entities.Cursos;
using MediatR;

namespace CQRS.Application.Handlers.Cursos
{

    public class CreateCursoCommandRequest :IRequest<Result<Guid>>
    {
        public required  string Titulo { get; set; }
        public required  string Descripcion { get; set;}
        public DateTime FechaPublicacion { get; set; }
        public decimal Precio { get; set; }
    }



    public class CreateCursoCommandHandler : IRequestHandler<CreateCursoCommandRequest, Result<Guid>>
    {
        private readonly ICursoRepository _cursoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCursoCommandHandler(ICursoRepository cursoRepository,IUnitOfWork unitOfWork)
        {
            _cursoRepository = cursoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateCursoCommandRequest request, CancellationToken cancellationToken)
        {
        
            var curso = Curso.Create(
                request.Titulo,
                request.Descripcion,
                request.FechaPublicacion,
                request.Precio               
                );
           
            await _cursoRepository.AgregarAsync(curso);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
         
            return Result.Success(curso.Id);
          
        }

       
    }
}
