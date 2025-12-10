using CQRS.Domain.Entities.Alumnos;
using CQRS.Domain.Entities.Cursos;
using CQRS.Domain.Entities.Matriculas;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Persistence.Data
{
    public class CQRSDbContextSeed
    {
        public static async Task InitialiseDatabaseAsync(CQRSDbContext context)
        {       
            context.Database.Migrate();
            await context.SaveChangesSeedAndMigrationDataAsync();
            await SeedAsync(context);
        }


        public static IEnumerable<Alumno> Alumnos => new List<Alumno>
        {
                Alumno.Create("Luis Miguel"),
                Alumno.Create("Junior Jhon")
        };
        public static IEnumerable<Curso> Cursos => new List<Curso>
        {
                Curso.Create("Curso de c#  de 0 a experto","Curso C#",new DateTime(2025, 1, 1),56),
                Curso.Create("Curso de Java de 0 a experto","Curso Java",new DateTime(2025, 12, 12),82)
        };

        public static IEnumerable<Matricula> Matriculas => new List<Matricula>
        {
                Matricula.Create(DateTime.Now,Alumnos.ToList()[0].Id,Cursos.ToList()[0].Id, "20251"),
                Matricula.Create(DateTime.Now,Alumnos.ToList()[1].Id,Cursos.ToList()[1].Id, "20251")
        };

        private static async Task SeedAsync(CQRSDbContext context)
        {
            
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
}
