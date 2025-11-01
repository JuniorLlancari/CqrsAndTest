using AutoMapper;
using CQRS.Application.DTOs;
using CQRS.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static CQRS.Application.Cursos.GetCursoQuery;

namespace CQRS.Application.Alumnos
{

    public class GetAlumnoQuery
    {
        public class GetAlumnoQueryRequest : IRequest<List<AlumnoDto>> { };

        public class GetAlumnoQueryHandler : IRequestHandler<GetAlumnoQueryRequest, List<AlumnoDto>>
        {
            private readonly CQRSDbContext _context;
            private readonly IMapper _mapper;

            public GetAlumnoQueryHandler(CQRSDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }


            public async Task<List<AlumnoDto>> Handle(GetAlumnoQueryRequest request, CancellationToken cancellationToken)
            {
                var alumnos = await _context.Alumnos.ToListAsync();
                return _mapper.Map<List<AlumnoDto>>(alumnos);
                 


            }
        }


    }
}
