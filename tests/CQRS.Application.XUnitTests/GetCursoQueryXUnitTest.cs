using CQRS.Application.Cursos;

namespace CQRS.Application.XUnitTests
{
    public class GetCursoQueryXUnitTest
    {

        private readonly GetCursoQuery.GetCursoQueryHandler _handlerAllCurso;

        public GetCursoQueryXUnitTest()
        {
            //// Fixture para datos de prueba
            //var fixture = new Fixture();
            //var cursoRecords = fixture.CreateMany<Curso>().ToList();
            //cursoRecords.Add(fixture.Build<Curso>().With(tr => tr.CursoId, Guid.Empty).Create());

            //// DbContext en memoria
            //var options = new DbContextOptionsBuilder<CQRSDbContext>()
            //    .UseInMemoryDatabase(databaseName: $"EducationDbContext-{Guid.NewGuid()}").Options;

            //var dbContextFake = new CQRSDbContext(options);
            //dbContextFake.Cursos.AddRange(cursoRecords);
            //dbContextFake.SaveChanges();

            //// AutoMapper
            //var mapConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingTest()));
            //var mapper = mapConfig.CreateMapper();

            //_handlerAllCurso = new GetCursoQuery.GetCursoQueryHandler(dbContextFake, mapper);
        }

        [Fact] // atributo para pruebas unitarias en xUnit
        public async Task GetCursoQueryHandler_ConsultaCursos_ReturnsTrue()
        {
            // Arrange
            var request = new GetCursoQuery.GetCursoQueryRequest();

            // Act
            var resultados = await _handlerAllCurso.Handle(request, CancellationToken.None);


            
            // Assert
            Assert.NotNull(resultados);
            Assert.NotEmpty(resultados); // opcional
        }
    }
}
