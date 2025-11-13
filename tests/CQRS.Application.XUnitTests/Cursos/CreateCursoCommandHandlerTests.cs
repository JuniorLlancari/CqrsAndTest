using CQRS.Application.Cursos;
using CQRS.Domain.Abstraccions;
using CQRS.Domain.Alumnos;
using CQRS.Domain.Cursos;
using Moq;

namespace CQRS.Application.XUnitTests.Cursos
{
    public class CreateCursoCommandHandlerTests
    {
        private readonly Mock<ICursoRepository> _cursoRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly CreateCursoCommandHandler _handler;
        public CreateCursoCommandHandlerTests()
        {
            _cursoRepositoryMock = new Mock<ICursoRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new CreateCursoCommandHandler(_cursoRepositoryMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldSuccess_WhenValidRequest()
        {
            //Arrange
            var request = new CreateCursoCommandRequest()
            {
                Titulo= "Curso C#",
                Descripcion= "Curso de c#  de 0 a experto",
                FechaPublicacion= new DateTime(2025, 1, 1),
                Precio = 20             
            };

            _cursoRepositoryMock.Setup(r=>r.AgregarAsync(It.IsAny<Curso>()))
                .Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>())).
                ReturnsAsync(1);

            //Act
            var result = await _handler.Handle(request, default);

            //Assert 
            Assert.True(result.IsSuccess);
        }
    }
}
