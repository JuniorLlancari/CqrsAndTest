using AutoMapper;
using CQRS.Application.DTOs;
using CQRS.Domain.Abstraccions;
using CQRS.Domain.Cursos;
using MediatR;

namespace CQRS.Application.Cursos
{
    
    public class GetCursoQueryRequest : IRequest<Result<List<CursoDto>>> { }


    public class GetCursoQueryHandler : IRequestHandler<GetCursoQueryRequest, Result<List<CursoDto>>>
    {
        private readonly ICursoRepository _cursoRepository;
        private readonly IMapper _mapper;

        public GetCursoQueryHandler(ICursoRepository cursoRepository, IMapper mapper)
        {
            _cursoRepository = cursoRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<CursoDto>>> Handle(GetCursoQueryRequest request, CancellationToken cancellationToken)
        {
            var cursos = await _cursoRepository.ListarAsync(a=>true);
            var cursosDto = _mapper.Map<List<CursoDto>>(cursos);
            return Result.Success(cursosDto);
        }
    }


    
}
