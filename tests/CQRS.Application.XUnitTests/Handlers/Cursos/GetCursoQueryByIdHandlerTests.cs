using AutoMapper;
using CQRS.Application.DTOs;
using CQRS.Application.Handlers.Cursos;
using CQRS.Domain.Entities.Cursos;
using Moq;

namespace CQRS.Application.XUnitTests.Handlers.Cursos
{
    public class GetCursoQueryByIdHandlerTests
    {
        private readonly Mock<ICursoRepository> _cursoRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetCursoQueryByIdHandler _handler;

        public GetCursoQueryByIdHandlerTests()
        {
            _cursoRepositoryMock = new Mock<ICursoRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetCursoQueryByIdHandler(_cursoRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenIdCursoExist()
        {
            // Arrange
            var curso = Curso.Create("Curso C#", "Curso de c#  de 0 a experto", new DateTime(2025, 1, 1), 56);
            var cursoDto = new CursoDto() { 
                Id = curso.Id,
                Descripcion= curso.Descripcion,
                FechaCreacion= curso.FechaCreacion,
                FechaPublicacion= curso.FechaPublicacion,
                Titulo=curso.Titulo,
                Precio=curso.Precio                           
            };
            var request = new GetCursoQueryByIdRequest() { Id = curso.Id };
            _cursoRepositoryMock
                .Setup(r => r.ObtenerPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(curso);

            _mapperMock.Setup(r => r.Map<CursoDto>(curso)).Returns(cursoDto);

            //Act
            var resultado = await _handler.Handle(request, default);

            //Assert
            Assert.True(resultado.IsSuccess);
        }





    }
}
