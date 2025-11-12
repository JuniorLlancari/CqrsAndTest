using CQRS.Application.Matriculas;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Application.XUnitTests.Matriculas
{
    public class CreateMatriculaCommandHandlerTests
    {
        //private static CQRSDbContext CreateInMemoryContext()
        //{
        //    var options = new DbContextOptionsBuilder<CQRSDbContext>()
        //        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        //        .Options;
        //    return new CQRSDbContext(options);
        //}
        //private static async Task<Curso> AddCursoAsync(CQRSDbContext context, Guid cursoId)
        //{
        //    var curso = new Curso
        //    {
        //        CursoId = cursoId,
        //        Titulo = "Curso Test",
        //        Descripcion = "Descripcion Test",
        //        FechaCreacion = DateTime.UtcNow,
        //        FechaPublicacion = DateTime.UtcNow,
        //        Precio = 100
        //    };
        //    context.Cursos.Add(curso);
        //    await context.SaveChangesSeedAndMigrationDataAsync();
        //    return curso;
        //}
        //private static async Task<Alumno> AddAlumnoAsync(CQRSDbContext context, Guid alumnoId)
        //{
        //    var alumno = new Alumno
        //    {
        //        AlumnoId = alumnoId,
        //        NombreAlumno = "Alumno Test"
        //    };
        //    context.Alumnos.Add(alumno);
        //    await context.SaveChangesSeedAndMigrationDataAsync();
        //    return alumno;
        //}

        //[Fact]
        //public async Task Handle_ShouldAddMatricula_WhenDataIsValid()
        //{
        //    // Arrange
        //    await using var context = CreateInMemoryContext();
        //    var cursoId = Guid.NewGuid();
        //    var alumnoId = Guid.NewGuid();
        //    var codigo = "MAT-001";

        //    await AddCursoAsync(context, cursoId);
        //    await AddAlumnoAsync(context, alumnoId);

        //    var handler = new CreateMatriculaCommand.CreateMatriculaCommandHanlder(context);
        //    var command = new CreateMatriculaCommand.CreateMatriculaCommandRequest
        //    {
        //        CursoId = cursoId,
        //        AlumnoId = alumnoId,
        //        Codigo = codigo,
        //        MatriculaId = Guid.NewGuid()
        //    };

        //    // Act
        //    var result = await handler.Handle(command, default);

        //    // Assert
        //    var matricula = await context.Matriculas.FirstOrDefaultAsync();
        //    Assert.NotNull(matricula);
        //    Assert.Equal(alumnoId, matricula.AlumnoId);
        //    Assert.Equal(cursoId, matricula.CursoId);
        //    Assert.Equal(codigo, matricula.Codigo);
        //}

        //[Fact]
        //public async Task Handle_ShouldThrow_WhenCursoDoesNotExist()
        //{
        //    // Arrange
        //    await using var context = CreateInMemoryContext();
        //    var alumno = await AddAlumnoAsync(context, Guid.NewGuid());
        //    var handler = new CreateMatriculaCommand.CreateMatriculaCommandHanlder(context);

        //    var command = new CreateMatriculaCommand.CreateMatriculaCommandRequest
        //    {
        //        CursoId = Guid.NewGuid(), // no existe
        //        AlumnoId = alumno.AlumnoId,
        //        Codigo = "MAT-001",
        //        MatriculaId = Guid.NewGuid()
        //    };

        //    // Act & Assert
        //    var ex = await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, default));
        //    Assert.Equal("Curso no existe", ex.Message);
        //}

        //[Fact]
        //public async Task Handle_ShouldThrow_WhenAlumnoDoesNotExist()
        //{
        //    // Arrange
        //    await using var context = CreateInMemoryContext();
        //    var curso = await AddCursoAsync(context, Guid.NewGuid());
        //    var handler = new CreateMatriculaCommand.CreateMatriculaCommandHanlder(context);

        //    var command = new CreateMatriculaCommand.CreateMatriculaCommandRequest
        //    {
        //        CursoId = curso.CursoId,
        //        AlumnoId = Guid.NewGuid(), // no existe
        //        Codigo = "MAT-001",
        //        MatriculaId = Guid.NewGuid()
        //    };

        //    // Act & Assert
        //    var ex = await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, default));
        //    Assert.Equal("Alumno no existe", ex.Message);
        //}

        //[Fact]
        //public async Task Handle_ShouldThrow_WhenAlumnoAlreadyMatriculado()
        //{
        //    // Arrange
        //    await using var context = CreateInMemoryContext();
        //    var cursoId = Guid.NewGuid();
        //    var alumnoId = Guid.NewGuid();
        //    var codigo = "MAT-001";

        //    await AddCursoAsync(context, cursoId);
        //    await AddAlumnoAsync(context, alumnoId);

        //    context.Matriculas.Add(new Matricula
        //    {
        //        CursoId = cursoId,
        //        AlumnoId = alumnoId,
        //        Codigo = codigo,
        //        FechaMatricula = DateTime.UtcNow,
        //        MatriculaId = Guid.NewGuid()
        //    });
        //    await context.SaveChangesSeedAndMigrationDataAsync();

        //    var handler = new CreateMatriculaCommand.CreateMatriculaCommandHanlder(context);
        //    var command = new CreateMatriculaCommand.CreateMatriculaCommandRequest
        //    {
        //        CursoId = cursoId,
        //        AlumnoId = alumnoId,
        //        Codigo = codigo,
        //        MatriculaId = Guid.NewGuid()
        //    };

        //    // Act & Assert
        //    var ex = await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, default));
        //    Assert.Equal("Error El alumno ya tiene el curso matriculado", ex.Message);
        //}
    }
}
