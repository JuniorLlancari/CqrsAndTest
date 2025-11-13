using CQRS.Application.Alumnos;
using CQRS.Application.Cursos;
using CQRS.Application.Matriculas;
using CQRS.Domain.Abstraccions;
using CQRS.Domain.Alumnos;
using CQRS.Domain.Cursos;
using CQRS.Domain.Matriculas;
using Moq;
using System.Linq.Expressions;

namespace CQRS.Application.XUnitTests.Matriculas
{
    public class CreateMatriculaCommandHandlerTests
    {
        private readonly Mock<IMatriculaRepository> _matriculaRepositoryMock;
        private readonly Mock<ICursoRepository> _cursoRepositoryMock;
        private readonly Mock<IAlumnoRepository> _alumnoRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        private readonly CreateMatriculaCommandHanlder _handler;

        public CreateMatriculaCommandHandlerTests()
        {
            _matriculaRepositoryMock = new Mock<IMatriculaRepository>();
            _cursoRepositoryMock = new Mock<ICursoRepository>();
            _alumnoRepositoryMock = new Mock<IAlumnoRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _handler = new CreateMatriculaCommandHanlder(
                _matriculaRepositoryMock.Object,
                _cursoRepositoryMock.Object,
                _alumnoRepositoryMock.Object,
                _unitOfWorkMock.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldFail_WhenCursoNotFound()
        {
            // Arrange
            var request = new CreateMatriculaCommandRequest
            {
                CursoId = Guid.NewGuid(),
                AlumnoId = Guid.NewGuid(),
                Codigo = "MAT-001"
            };

            _cursoRepositoryMock
                .Setup(r => r.ObtenerPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Curso?)null);

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(CursoError.NoEncontrado, result.Error);
        }

        [Fact]
        public async Task Handle_ShouldFail_WhenAlumnoNotFound()
        {
            // Arrange
            var request = new CreateMatriculaCommandRequest
            {
                CursoId = Guid.NewGuid(),
                AlumnoId = Guid.NewGuid(),
                Codigo = "MAT-001"
            };

            _cursoRepositoryMock
                .Setup(r => r.ObtenerPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Curso { Id = request.CursoId });

            _alumnoRepositoryMock
                .Setup(r => r.ObtenerPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Alumno?)null);

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(AlumnoError.NoEncontrado, result.Error);
        }

        [Fact]
        public async Task Handle_ShouldFail_WhenAlumnoAlreadyMatriculado()
        {
            // Arrange
            var request = new CreateMatriculaCommandRequest
            {
                CursoId = Guid.NewGuid(),
                AlumnoId = Guid.NewGuid(),
                Codigo = "MAT-001"
            };

            _cursoRepositoryMock
                .Setup(r => r.ObtenerPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Curso { Id = request.CursoId });

            _alumnoRepositoryMock
                .Setup(r => r.ObtenerPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Alumno { Id = request.AlumnoId });

            _matriculaRepositoryMock
                .Setup(r => r.ObtenerPorFiltro(It.IsAny<Expression<Func<Matricula, bool>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Matricula()); // ya existe

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(MatriculaError.TieneMatriculaActiva, result.Error);
        }

        [Fact]
        public async Task Handle_ShouldSuccess_WhenValidRequest()
        {
            // Arrange
            var request = new CreateMatriculaCommandRequest
            {
                CursoId = Guid.NewGuid(),
                AlumnoId = Guid.NewGuid(),
                Codigo = "MAT-001"
            };

            _cursoRepositoryMock
                .Setup(r => r.ObtenerPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Curso { Id = request.CursoId });

            _alumnoRepositoryMock
                .Setup(r => r.ObtenerPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Alumno { Id = request.AlumnoId });

            _matriculaRepositoryMock
                .Setup(r => r.ObtenerPorFiltro(
                    It.IsAny<Expression<Func<Matricula, bool>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()
                ))
            .ReturnsAsync((Matricula?)null);

            _matriculaRepositoryMock
                .Setup(r => r.AgregarAsync(It.IsAny<Matricula>()))
                .Returns(Task.CompletedTask);

            _unitOfWorkMock
                .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(request, default);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotEqual(Guid.Empty, result.Value);



            _alumnoRepositoryMock.Verify(
            r => r.ObtenerPorIdAsync(request.AlumnoId, It.IsAny<CancellationToken>()),
            Times.Once);

            _cursoRepositoryMock.Verify(
                r => r.ObtenerPorIdAsync(request.CursoId, It.IsAny<CancellationToken>()),
                Times.Once);

            _matriculaRepositoryMock.Verify(
                r => r.ObtenerPorFiltro(
                    It.IsAny<Expression<Func<Matricula, bool>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);

            _matriculaRepositoryMock.Verify(
                r => r.AgregarAsync(It.IsAny<Matricula>()),
                Times.Once);

            _unitOfWorkMock.Verify(
                u => u.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Once);





        }
    }
}
