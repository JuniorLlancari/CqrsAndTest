using AutoMapper;
using CQRS.Application.DTOs;
using CQRS.Application.Handlers.Matriculas;
using CQRS.Domain.Entities.Matriculas;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.XUnitTests.Handlers.Matriculas
{
    public class GetMatriculaQueryHandlerTests
    {
        private readonly Mock<IMatriculaRepository> _matriculaRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetMatriculaQueryHandler _handler;

        public GetMatriculaQueryHandlerTests()
        {
            _matriculaRepositoryMock = new Mock<IMatriculaRepository>();
            _mapperMock = new Mock<IMapper>();

            _handler = new GetMatriculaQueryHandler(
                _matriculaRepositoryMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenMatriculasExist()
        {
            // Arrange
            var matriculas = new List<Matricula>
            {
                Matricula.Create(DateTime.UtcNow, Guid.NewGuid(), Guid.NewGuid(), "MAT-001"),
                Matricula.Create(DateTime.UtcNow, Guid.NewGuid(), Guid.NewGuid(), "MAT-002")
            };

            var matriculasDto = new List<MatriculaDto>
            {
                new() { Id = matriculas[0].Id, Codigo = "MAT-001" },
                new() { Id = matriculas[1].Id, Codigo = "MAT-002" }
            };


            _matriculaRepositoryMock
                .Setup(r => r.ListarAsync(
                    It.IsAny<Expression<Func<Matricula, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string>()
                ))
                .ReturnsAsync(matriculas);

            _mapperMock
                .Setup(m => m.Map<List<MatriculaDto>>(matriculas))
                .Returns(matriculasDto);

            var request = new GetMatriculaQueryRequest();

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(2, result.Value.Count);
            Assert.Equal("MAT-001", result.Value[0].Codigo);

            _matriculaRepositoryMock.Verify(
                r => r.ListarAsync(
                    It.IsAny<Expression<Func<Matricula, bool>>>(),
                false, "Curso,Alumno"),
                Times.Once);

            _mapperMock.Verify(
                m => m.Map<List<MatriculaDto>>(It.IsAny<List<Matricula>>()),
                Times.Once);
        }
    }
}
