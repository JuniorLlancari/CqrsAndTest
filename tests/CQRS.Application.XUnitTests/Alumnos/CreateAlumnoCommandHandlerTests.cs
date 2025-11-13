using CQRS.Application.Alumnos;
using CQRS.Domain.Abstraccions;
using CQRS.Domain.Alumnos;
using Moq;

namespace CQRS.Application.XUnitTests.Alumnos
{
    public class CreateAlumnoCommandHandlerTests
    {
        private readonly Mock<IAlumnoRepository> _alumnoRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly CreateAlumnoCommandHandler _handler;

        public CreateAlumnoCommandHandlerTests()
        {
            _alumnoRepositoryMock = new Mock<IAlumnoRepository>();
            _unitOfWorkMock= new Mock<IUnitOfWork>();
            _handler= new CreateAlumnoCommandHandler(_alumnoRepositoryMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldSuccess_WhenValidRequest()
        {
            var request = new CreateAlumnoCommandRequest()
            {
                NombreAlumno = "Junior Jhonatan"
            };

            _alumnoRepositoryMock.Setup(r => r.AgregarAsync(It.IsAny<Alumno>()));
            _unitOfWorkMock.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()));



            var result = await _handler.Handle(request, default);


            Assert.NotNull(result);
            Assert.True(result.IsSuccess);




        }





    }
}
