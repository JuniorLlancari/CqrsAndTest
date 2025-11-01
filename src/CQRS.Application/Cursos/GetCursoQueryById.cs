using AutoMapper;
using CQRS.Application.DTOs;
using CQRS.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Application.Cursos
{
    public class GetCursoQueryById
    {
        public class GetCursoQueryByIdRequest : IRequest<CursoDto> 
        {
            public Guid Id { get; set; }
        }

        public class GetCursoQueryByIdHandler : IRequestHandler<GetCursoQueryByIdRequest, CursoDto>
        {
            private readonly CQRSDbContext _context;
            private readonly IMapper _mapper;

            public GetCursoQueryByIdHandler(CQRSDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CursoDto> Handle(GetCursoQueryByIdRequest request, CancellationToken cancellationToken)
            {
                var curso = await _context.Cursos.FirstOrDefaultAsync(x => x.CursoId == request.Id);
                var cursoDto = _mapper.Map<CursoDto>(curso);
                return cursoDto;
            }
        }




    }
}
