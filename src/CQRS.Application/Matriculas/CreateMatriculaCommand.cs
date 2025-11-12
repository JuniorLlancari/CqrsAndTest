using CQRS.Application.Alumnos;
using CQRS.Application.Cursos;
using CQRS.Domain.Abstraccions;
using CQRS.Domain.Alumnos;
using CQRS.Domain.Cursos;
using CQRS.Domain.Matriculas;
using MediatR;
using System.Linq.Expressions;

namespace CQRS.Application.Matriculas
{
    public class CreateMatriculaCommandRequest : IRequest<Result<Guid>> 
    {
        public Guid CursoId { get; set; }
        public Guid AlumnoId { get; set; }
        public string Codigo { get; set; }

    }

    public class CreateMatriculaCommandHanlder : IRequestHandler<CreateMatriculaCommandRequest, Result<Guid>>
    {
        private readonly IMatriculaRepository _matriculaRepository;
        private readonly ICursoRepository _cursoRepository;
        private readonly IAlumnoRepository _alumnoRepository;
        private readonly IUnitOfWork _unitOfWork;



        public CreateMatriculaCommandHanlder(
            IMatriculaRepository matriculaRepository,
            ICursoRepository cursoRepository,
            IAlumnoRepository alumnoRepository, 
            IUnitOfWork unitOfWork
                )
        {
            _unitOfWork = unitOfWork;
            _matriculaRepository = matriculaRepository;
            _cursoRepository = cursoRepository;
            _alumnoRepository = alumnoRepository;
        }

        public  async Task<Result<Guid>> Handle(CreateMatriculaCommandRequest request, CancellationToken cancellationToken)
        {
            var curso = await _cursoRepository.ObtenerPorIdAsync(request.CursoId, cancellationToken);
            if (curso == null)
            {
                return Result.Failure<Guid>(CursoError.NoEncontrado);
            }

            var alumno = await _alumnoRepository.ObtenerPorIdAsync(request.AlumnoId, cancellationToken);
            if (alumno == null)
            {
                return Result.Failure<Guid>(AlumnoError.NoEncontrado);
            }



            Expression<Func<Matricula, bool>> filtro = cu =>
                cu.CursoId == request.CursoId &&
                cu.AlumnoId == request.AlumnoId &&
                cu.Codigo == request.Codigo;

            var alumnoRegistrado = await _matriculaRepository.ObtenerPorFiltro(filtro);

            if (alumnoRegistrado != null)
            {
                return Result.Failure<Guid>(MatriculaError.TieneMatriculaActiva);
            }





            var matricula = Matricula.Create(DateTime.Now, request.AlumnoId, request.CursoId, request.Codigo);
                
            await _matriculaRepository.AgregarAsync(matricula);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success(matricula.Id);





        }
    }
}
