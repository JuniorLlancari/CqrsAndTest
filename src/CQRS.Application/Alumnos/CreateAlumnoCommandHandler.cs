using AutoMapper;
using CQRS.Domain.Abstraccions;
using CQRS.Domain.Alumnos;
using MediatR;

namespace CQRS.Application.Alumnos
{
    public class CreateAlumnoCommandRequest :IRequest<Result<Guid>>
    {
        public string NombreAlumno { get; set; }
    }
    public class CreateAlumnoResponse
    {
        public string NombreAlumno { get; set; }
    }

    public class CreateAlumnoCommandHandler : IRequestHandler<CreateAlumnoCommandRequest, Result<Guid>>
    {
        private readonly IAlumnoRepository _alumnoRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateAlumnoCommandHandler(IAlumnoRepository repository, IUnitOfWork unitOfWork)
        {
            _alumnoRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateAlumnoCommandRequest request, CancellationToken cancellationToken)
        {
            var alumno = Alumno.Create(request.NombreAlumno);
            await _alumnoRepository.AgregarAsync(alumno);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(alumno.Id);
        }  
    }
}
