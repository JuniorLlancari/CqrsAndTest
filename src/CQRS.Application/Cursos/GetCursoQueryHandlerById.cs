using AutoMapper;
using CQRS.Application.DTOs;
using CQRS.Domain.Abstraccions;
using CQRS.Domain.Cursos;
using MediatR;

namespace CQRS.Application.Cursos
{

    public class GetCursoQueryByIdRequest : IRequest<Result<CursoDto>> 
    {
        public Guid Id { get; set; }
    }

    public class GetCursoQueryByIdHandler : IRequestHandler<GetCursoQueryByIdRequest, Result<CursoDto>>
    {
        private readonly ICursoRepository _cursoRepository;
        private readonly IMapper _mapper;

        public GetCursoQueryByIdHandler(ICursoRepository cursoRepository, IMapper mapper)
        {
            _cursoRepository = cursoRepository;
            _mapper = mapper;
        }

        public async Task<Result<CursoDto>> Handle(GetCursoQueryByIdRequest request, CancellationToken cancellationToken)
        {
            var curso = await _cursoRepository.ListarAsync(x => true);
            var cursoDto = _mapper.Map<CursoDto>(curso);
            return Result.Success(cursoDto);
        }
    }

}
