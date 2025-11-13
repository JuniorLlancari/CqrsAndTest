using AutoMapper;
using CQRS.Application.Alumnos;
using CQRS.Application.DTOs;
using CQRS.Domain.Alumnos;
using CQRS.Domain.Matriculas;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.XUnitTests.Alumnos
{
    public class GetAlumnoQueryHandlerTests
    {
        private readonly Mock<IAlumnoRepository> _alumnoRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetAlumnoQueryHandler _handler;


        public GetAlumnoQueryHandlerTests()
        {
            _alumnoRepositoryMock = new Mock<IAlumnoRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetAlumnoQueryHandler(_alumnoRepositoryMock.Object,
                _mapperMock.Object);

        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenAlumnosExist()
        {
            // Arrange
            var alumnos = new List<Alumno> {
                Alumno.Create("Junior Jhonatan"),
                Alumno.Create("Luis Miguel")
            };

            var alumnosDto = new List<AlumnoDto> {
                new(){ Id=alumnos[0].Id,NombreAlumno=alumnos[0].NombreAlumno },
                new(){ Id=alumnos[1].Id,NombreAlumno=alumnos[1].NombreAlumno },
            };

            _alumnoRepositoryMock.Setup(r => r.ListarAsync(
                It.IsAny<Expression<Func<Alumno, bool>>>(),
                It.IsAny<bool>(),
                It.IsAny<string>()
                )).ReturnsAsync(alumnos);
 
            _mapperMock
                .Setup( m => m.Map<List<AlumnoDto>>(alumnos))  
                .Returns(alumnosDto);

            var request = new GetAlumnoQueryRequest();
            
            // Act
            var result =  await _handler.Handle(request, default);

            // Assert

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(2,result.Value.Count);
            Assert.Equal("Junior Jhonatan", result.Value[0].NombreAlumno);


            _alumnoRepositoryMock.Verify(
                r => r.ListarAsync( a=>true, false, null ),
                Times.Once);

            _mapperMock.Verify(
                m => m.Map<List<AlumnoDto>>(It.IsAny<List<Alumno>>()),
                Times.Once);


        }



    }
}
