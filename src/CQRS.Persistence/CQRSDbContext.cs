using CQRS.Domain.Abstraccions;
using CQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Persistence
{
    public class CQRSDbContext : DbContext, IUnitOfWork
    {
        public CQRSDbContext()
        {
                
        }

        public CQRSDbContext(DbContextOptions<CQRSDbContext> options):base(options)
        {
            
        }

        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Curso>().Property(c => c.Precio).HasPrecision(14, 2);

            modelBuilder.Entity<Alumno>().Property(c => c.NombreAlumno).HasMaxLength(25);

            // Configuración de relaciones
            modelBuilder.Entity<Matricula>()
                .HasOne(m => m.Alumno)
                .WithMany(a => a.Matriculas)
                .HasForeignKey(m => m.AlumnoId);

            modelBuilder.Entity<Matricula>()
                .HasOne(m => m.Curso)
                .WithMany(c => c.Matriculas)
                .HasForeignKey(m => m.CursoId);

            var fechaCreacion = new DateTime(2025, 1, 1);
            var fechaPublicacion = new DateTime(2025, 12, 12);

            modelBuilder.Entity<Curso>().HasData(
                new Curso
                {
                    CursoId = new Guid("11111111-1111-1111-1111-111111111111"),
                    Descripcion = "Curso de c#  de 0 a experto",
                    Titulo= "Curso C#",
                    FechaCreacion= fechaCreacion,
                    FechaPublicacion= fechaPublicacion,
                    Precio=56
                }               
            );
            modelBuilder.Entity<Curso>().HasData(
                new Curso
                {
                    CursoId = new Guid("22222222-2222-2222-2222-222222222222"),
                    Descripcion = "Curso de Java de 0 a experto",
                    Titulo = "Curso Java",
                    FechaCreacion = fechaCreacion,
                    FechaPublicacion = fechaPublicacion,
                    Precio = 82
                }
            );


            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                var result = await base.SaveChangesAsync(cancellationToken);
                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbUpdateConcurrencyException("La excepcion por concurrencia se disparo", ex);
            }
        }

        public async Task<int> SaveChangesSeedAndMigrationDataAsync()
        {
            try
            {
                var result = await base.SaveChangesAsync();
                return result;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

    }
}
