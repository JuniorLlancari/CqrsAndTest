using AutoMapper;
using CQRS.Application.DTOs;
using CQRS.Domain.Entities.Alumnos;
using CQRS.Domain.Entities.Cursos;
using CQRS.Domain.Entities.Matriculas;

namespace CQRS.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Curso,CursoDto>().ReverseMap();
            CreateMap<Alumno, AlumnoDto>().ReverseMap();
            CreateMap<Matricula, MatriculaDto>()
                .ForMember(dest => dest.FechaMatricula, opt => opt.MapFrom(src => src.FechaMatricula))
                .ForMember(dest => dest.NombreCurso, opt => opt.MapFrom(src => src.Curso.Titulo))
                .ForMember(dest => dest.NombreAlumno, opt => opt.MapFrom(src => src.Alumno.NombreAlumno));

        }
    }
}
