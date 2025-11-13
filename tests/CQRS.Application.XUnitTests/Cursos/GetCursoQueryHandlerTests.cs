using AutoMapper;
using CQRS.Application.Cursos;
using CQRS.Application.DTOs;
using CQRS.Domain.Cursos;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.XUnitTests.Cursos
{
    public class GetCursoQueryHandlerTests
    {
        private readonly Mock<ICursoRepository> _cursoRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetCursoQueryHandler _handler;

        public GetCursoQueryHandlerTests()
        {
            _cursoRepositoryMock = new Mock<ICursoRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetCursoQueryHandler(_cursoRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_DeberiaRetornarListaDeCursoDto()
        {
            // Arrange
 
            var cursos = new List<Curso>
            {
                Curso.Create("Curso C#", "Curso de C# de 0 a experto",  new DateTime(2025, 1, 1), 56),
                Curso.Create("Curso F#", "Curso de F# de 0 a experto",  new DateTime(2025, 1, 1), 56)
            };

            var cursosDto = new List<CursoDto>
            {
                new CursoDto { 
                    Id = cursos[0].Id,
                    Descripcion = cursos[0].Descripcion,
                    Titulo= cursos[0].Titulo,
                    FechaPublicacion = cursos[0].FechaPublicacion,
                    Precio= cursos[0].Precio,
                },
                new CursoDto {
                    Id = cursos[1].Id,
                    Descripcion = cursos[1].Descripcion,
                    Titulo= cursos[1].Titulo,
                    FechaPublicacion = cursos[1].FechaPublicacion,
                    Precio= cursos[1].Precio,
                },            
            };

            _cursoRepositoryMock
                .Setup(r => r.ListarAsync(
                    It.IsAny<Expression<Func<Curso, bool>>>(), 
                    It.IsAny<bool>(),
                    It.IsAny<string>()        
                ))
                .ReturnsAsync(cursos);

            _mapperMock
                .Setup(m => m.Map<List<CursoDto>>(cursos))
                .Returns(cursosDto);

            var request = new GetCursoQueryRequest();

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(2, result.Value.Count);
            Assert.Equal("Curso C#", result.Value[0].Titulo);

            // Verify
            _cursoRepositoryMock.Verify(
                r => r.ListarAsync(a => true,false,null),
                Times.Once);

            _mapperMock.Verify(
                m => m.Map<List<CursoDto>>(It.IsAny<List<Curso>>()),
                Times.Once);
        }
    }
}
