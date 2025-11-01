using AutoMapper;
using CQRS.Application.DTOs;
using CQRS.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Matriculas
{
    public class GetMatriculaQuery
    {
        public class GetMatriculaQueryRequest : IRequest<List<MatriculaDto>> { }

        public class GetMatriculaQueryHandler : IRequestHandler<GetMatriculaQueryRequest, List<MatriculaDto>>
        {

            private readonly CQRSDbContext _context;
            private readonly IMapper _mapper;

            public GetMatriculaQueryHandler(CQRSDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<MatriculaDto>> Handle(GetMatriculaQueryRequest request, CancellationToken cancellationToken)
            {
                var matriculas = await _context.Matriculas.ToListAsync(cancellationToken: cancellationToken);
                var matriculasDto = _mapper.Map<List<MatriculaDto>>(matriculas);

                return matriculasDto;


            }
        }


    }
}
