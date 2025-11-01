using AutoFixture;
using AutoMapper;
using CQRS.Application.Cursos;
using CQRS.Application.NUnitTests.Helper;
using CQRS.Domain.Entities;
using CQRS.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace CQRS.Application.NUnitTests
{
    public class CreateCursoCommandNUnitTest
    {
        private CreateCursoCommandHandler handlerCreateCurso;

        [SetUp]
        public void Setup()
        {
            var fixture = new Fixture();
            var cursoRecords = fixture.CreateMany<Curso>().ToList();
            cursoRecords.Add(fixture.Build<Curso>().With(tr => tr.CursoId, Guid.Empty).Create());

            var options = new DbContextOptionsBuilder<CQRSDbContext>()
                .UseInMemoryDatabase(databaseName: $"EducationDbCotext-{Guid.NewGuid()}").Options;

            var dbContextFake = new CQRSDbContext(options);
            dbContextFake.Cursos.AddRange(cursoRecords);
            dbContextFake.SaveChanges();

            var mapConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingTest()));

            var mapper = mapConfig.CreateMapper();

            handlerCreateCurso = new CreateCursoCommandHandler(dbContextFake);



        }

        [Test]
        public async Task GetCursoQueryHandler_ConsultaCursos_ReturnsTrue()
        {
            CreateCursoCommandRequest request = new();
            request.FechaPublicacion = DateTime.UtcNow.AddDays(59);
            request.Titulo = "Libro de Preuba";
            request.Descripcion = "Aprende a crear unit test";
            request.Precio = 100;
            // Arrange
            var resultados = await handlerCreateCurso.Handle(request, CancellationToken.None);
            // Assert
            Assert.That(resultados, Is.EqualTo(Unit.Value));
        }


    }
}
