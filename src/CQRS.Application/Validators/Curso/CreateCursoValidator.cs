using CQRS.Application.Handlers.Cursos;
using FluentValidation;

namespace CQRS.Application.Validators.Curso
{
    public class CreateCursoCommandRequestValidator : AbstractValidator<CreateCursoCommandRequest>
    {
        public CreateCursoCommandRequestValidator()
        {
            RuleFor(x => x.Descripcion).NotEmpty().WithMessage("La Descripcion es Requerido");
            RuleFor(x => x.Titulo).NotEmpty().WithMessage("El Titulo es Requerido");
        }
    }
}
