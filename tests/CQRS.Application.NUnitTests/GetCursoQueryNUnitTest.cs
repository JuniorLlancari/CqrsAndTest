using CQRS.Application.Cursos;
using NUnit.Framework;

namespace CQRS.Application.NUnitTests
{
    [TestFixture]
    public class GetCursoQueryNUnitTest
    {
        private GetCursoQuery.GetCursoQueryHandler handlerAllCurso;

        [SetUp]
        public void Setup()
        {
            //var fixture = new Fixture();
            //var cursoRecords = fixture.CreateMany<Curso>().ToList();
            //cursoRecords.Add(fixture.Build<Curso>().With(tr => tr.CursoId, Guid.Empty).Create());

            //var options = new DbContextOptionsBuilder<CQRSDbContext>()
            //    .UseInMemoryDatabase(databaseName: $"EducationDbCotext-{Guid.NewGuid()}").Options;

            //var dbContextFake = new CQRSDbContext(options);
            //dbContextFake.Cursos.AddRange(cursoRecords);
            //dbContextFake.SaveChanges();

            //var mapConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingTest()));

            //var mapper=mapConfig.CreateMapper();

            //handlerAllCurso = new GetCursoQuery.GetCursoQueryHandler(dbContextFake, mapper);



        }

        [Test]
        public async Task GetCursoQueryHandler_ConsultaCursos_ReturnsTrue()
        {
            GetCursoQuery.GetCursoQueryRequest request = new();



            // Arrange
             var resultados = await handlerAllCurso.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsNotNull(resultados);
        }

    }
}
