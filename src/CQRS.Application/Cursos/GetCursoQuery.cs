using AutoMapper;
using CQRS.Application.DTOs;
using CQRS.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Application.Cursos
{
    public class GetCursoQuery
    {
        public class GetCursoQueryRequest : IRequest<List<CursoDto>>{ }


        public class GetCursoQueryHandler : IRequestHandler<GetCursoQueryRequest, List<CursoDto>>
        {
            private readonly CQRSDbContext _context;
            private readonly IMapper _mapper;

            public GetCursoQueryHandler(CQRSDbContext context , IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<CursoDto>> Handle(GetCursoQueryRequest request, CancellationToken cancellationToken)
            {
                var cursos = await _context.Cursos.ToListAsync();
                var cursosDto = _mapper.Map<List<CursoDto>>(cursos);
                return cursosDto;
            }
        }


    }
}
