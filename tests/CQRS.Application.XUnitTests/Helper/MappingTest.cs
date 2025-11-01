using AutoMapper;
using CQRS.Application.DTOs;
using CQRS.Domain.Entities;

namespace CQRS.Application.XUnitTests.Helper
{
    public class MappingTest : Profile
    {
        public MappingTest()
        {
            CreateMap<Curso, CursoDto>();
        }

    }
}
