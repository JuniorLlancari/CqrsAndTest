using CQRS.Domain.Abstraccions;
using CQRS.Domain.Alumnos;
using CQRS.Domain.Cursos;
using CQRS.Domain.Matriculas;
using CQRS.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistencie(this IServiceCollection services, IConfiguration configuration)
        {

            //var connectionString = configuration.GetConnectionString("DefaultConnection");
            //services.AddDbContext<CQRSDbContext>(options =>
            //    options.UseSqlServer(connectionString));

            services.AddDbContext<CQRSDbContext>(options =>
                options.UseInMemoryDatabase("CqrsInMemoryDb"));



            services.AddScoped<IAlumnoRepository, AlumnoRepository>();
            services.AddScoped<IMatriculaRepository, MatriculaRepository>();
            services.AddScoped<ICursoRepository, CursoRepository>();
            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<CQRSDbContext>());





            return services;
        }

    }
}
