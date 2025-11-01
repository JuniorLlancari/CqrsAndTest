using AutoMapper;
using CQRS.Domain;
using CQRS.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Application.Alumnos
{
    public class CreateAlumnoRequest :IRequest
    {
        public Guid AlumnoId { get; set; }
        public string NombreAlumno { get; set; }
    }
    public class CreateAlumnoResponse
    {
        public Guid AlumnoId { get; set; }
        public string NombreAlumno { get; set; }
    }

    public class CreateAlumnoCommandHandler : IRequestHandler<CreateAlumnoRequest>
    {
        private readonly CQRSDbContext _context;
        private readonly IMapper _mapper;

        public CreateAlumnoCommandHandler(CQRSDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<Unit> Handle(CreateAlumnoRequest request, CancellationToken cancellationToken)
        {
            var alumno = new Alumno()
            {
                AlumnoId = request.AlumnoId,
                NombreAlumno = request.NombreAlumno
            };


            _context.Add(alumno);
            var respuesta = await _context.SaveChangesAsync(cancellationToken);
            if (respuesta > 0)
            {
                return Unit.Value;
            }

            throw new NotImplementedException();
        }
    }
}
