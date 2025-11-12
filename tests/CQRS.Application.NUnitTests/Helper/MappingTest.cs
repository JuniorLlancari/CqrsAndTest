using AutoMapper;
using CQRS.Application.DTOs;
using CQRS.Domain.Cursos;

namespace CQRS.Application.NUnitTests.Helper
{
    public class MappingTest :Profile
    {
        public MappingTest() 
        {
            CreateMap<Curso, CursoDto>();
        }

    }
}
