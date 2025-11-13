using AutoMapper;
using CQRS.Application.DTOs;
using CQRS.Domain.Abstraccions;
using CQRS.Domain.Matriculas;
using MediatR;

namespace CQRS.Application.Matriculas
{
 
    public class GetMatriculaQueryRequest : IRequest<Result<List<MatriculaDto>>> { }

    public class GetMatriculaQueryHandler : IRequestHandler<GetMatriculaQueryRequest, Result<List<MatriculaDto>>>
    {

        private readonly IMatriculaRepository _matriculaRepository;
        private readonly IMapper _mapper;

        public GetMatriculaQueryHandler(IMatriculaRepository matriculaRepository, IMapper mapper)
        {
            _matriculaRepository= matriculaRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<MatriculaDto>>> Handle(GetMatriculaQueryRequest request, CancellationToken cancellationToken)
        {
            var matriculas = await _matriculaRepository.ListarAsync(a => true,false,"Curso,Alumno");
            var matriculasDto = _mapper.Map<List<MatriculaDto>>(matriculas);
            return Result.Success(matriculasDto);
        }
    }
    
}
