using CQRS.Domain.Abstraccions;
using CQRS.Domain.Entities.Alumnos;
using CQRS.Domain.Entities.Cursos;
using CQRS.Domain.Entities.Matriculas;
using CQRS.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CQRS.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistencie(this IServiceCollection services, IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<CQRSDbContext>(options =>
            options.UseSqlServer(configuration["CNSTRINGDBSQLSERVER"]));
            //options.UseSqlServer(connectionString));

            //services.AddDbContext<CQRSDbContext>(options =>
            //    options.UseInMemoryDatabase("CqrsInMemoryDb"));



            services.AddScoped<IAlumnoRepository, AlumnoRepository>();
            services.AddScoped<IMatriculaRepository, MatriculaRepository>();
            services.AddScoped<ICursoRepository, CursoRepository>();
            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<CQRSDbContext>());





            return services;
        }

    }
}
