using CQRS.Domain.Entities.Alumnos;
using CQRS.Domain.Entities.Cursos;
using CQRS.Domain.Entities.Matriculas;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace CQRS.Persistence.Data;

public static class DataSeeder
{

    public async static Task CreateDbIfNotExists(IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                await SeedeApplicationDB(services);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILoggerFactory>().CreateLogger("DataSeeder");
                logger.LogError(ex, "An error occurred creating the DB.");
            }
        }
    }





    private static async Task SeedeApplicationDB(IServiceProvider services)
    {
        var context = services.GetRequiredService<CQRSDbContext>();
        await context.Database.MigrateAsync();

        if (await context.Cursos.AnyAsync() ||
                await context.Alumnos.AnyAsync() ||
                await context.Matriculas.AnyAsync())
            return;




        var alumnos = new List<Alumno>
        {
            Alumno.Create("Luis Miguel"),
            Alumno.Create("Junior Jhon")
        };


        var cursos = new List<Curso>
        {
            Curso.Create("Curso de c# de 0 a experto","Curso C#", new DateTime(2025, 1, 1), 56),
            Curso.Create("Curso de Java de 0 a experto","Curso Java", new DateTime(2025, 12, 12), 82)
        };

        await context.Alumnos.AddRangeAsync(alumnos);
        await context.Cursos.AddRangeAsync(cursos);
        await context.SaveChangesSeedAndMigrationDataAsync();


        var matriculas = new List<Matricula>
        {
            Matricula.Create(DateTime.Now, alumnos[0].Id, cursos[0].Id, "20251"),
            Matricula.Create(DateTime.Now, alumnos[1].Id, cursos[1].Id, "20251")
        };

        await context.Matriculas.AddRangeAsync(matriculas);
        await context.SaveChangesSeedAndMigrationDataAsync();


    }
}
