using AutoMapper;
using CQRS.Application.DTOs;
using CQRS.Domain.Abstraccions;
using CQRS.Domain.Alumnos;
using MediatR;

namespace CQRS.Application.Alumnos
{

   
        public class GetAlumnoQueryRequest : IRequest<Result<List<AlumnoDto>>> { };

        public class GetAlumnoQueryHandler : IRequestHandler<GetAlumnoQueryRequest, Result<List<AlumnoDto>>>
        {
            private readonly  IAlumnoRepository _alumnoRepository;
            private readonly IMapper _mapper;
            public GetAlumnoQueryHandler(IAlumnoRepository alumnoRepository, IMapper mapper)
            {
                _alumnoRepository = alumnoRepository;
                _mapper = mapper;
            }


            public async Task<Result<List<AlumnoDto>>> Handle(GetAlumnoQueryRequest request, CancellationToken cancellationToken)
            {
                var alumnos = await _alumnoRepository.ListarAsync(a => true);
                var resultado = _mapper.Map<List<AlumnoDto>>(alumnos);
                return Result.Success(resultado);
            }
        }


    
}
