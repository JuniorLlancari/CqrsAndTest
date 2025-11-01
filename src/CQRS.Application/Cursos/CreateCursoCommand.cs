using CQRS.Domain;
using CQRS.Persistence;
using FluentValidation;
using MediatR;

namespace CQRS.Application.Cursos
{
    public class CreateCursoCommandRequest :IRequest
    {
        public Guid CursoId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set;}
        public DateTime FechaPublicacion { get; set; }
        public decimal Precio { get; set; }

    }

    public class CreateCursoCommandRequestValidator: AbstractValidator<CreateCursoCommandRequest>
    {
        public CreateCursoCommandRequestValidator()
        {
            RuleFor(x => x.Descripcion);
            RuleFor(x => x.Titulo);
        }
    }



    public class CreateCursoCommandHandler : IRequestHandler<CreateCursoCommandRequest>
    {
        private readonly CQRSDbContext _context;
        public CreateCursoCommandHandler(CQRSDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateCursoCommandRequest request, CancellationToken cancellationToken)
        {
            var curso = new Curso()
            {
                CursoId = Guid.NewGuid(),
                Titulo = request.Titulo,
                Descripcion = request.Descripcion,
                FechaCreacion = DateTime.UtcNow,
                FechaPublicacion = request.FechaPublicacion,
                Precio = request.Precio,
            };
            _context.Add(curso);
            var respuesta = await _context.SaveChangesAsync();
            if (respuesta > 0)
            {
                return Unit.Value;
            }

            throw new NotImplementedException();
        }
    }
}
