using AutoMapper;
using CQRS.Application.DTOs;
using CQRS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
