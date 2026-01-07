using CQRS.Application.Abstractions.Events;
using CQRS.Domain.Abstraccions;
using CQRS.Domain.Entities.Alumnos;
using CQRS.Domain.Entities.Cursos;
using CQRS.Domain.Entities.Matriculas;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Persistence
{
    public class CQRSDbContext : DbContext, IUnitOfWork
    {
        public readonly IPublisher _publisher;

        public CQRSDbContext() {}

        public CQRSDbContext(DbContextOptions<CQRSDbContext> options):base(options){ }
        public CQRSDbContext(DbContextOptions<CQRSDbContext> options, IPublisher publisher) : base(options) 
        {
            _publisher = publisher;
        }

        public virtual  DbSet<Curso> Cursos { get; set; }
        public virtual  DbSet<Alumno> Alumnos { get; set; }
        public virtual DbSet<Matricula> Matriculas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Curso>().Property(c => c.Precio).HasPrecision(14, 2);

            modelBuilder.Entity<Alumno>().Property(c => c.NombreAlumno).HasMaxLength(25);

            modelBuilder.Entity<Alumno>().Property(usuario => usuario.Estado)
                .HasConversion(
                    estado => estado.ToString(),
                    estado => (AlumnoEstado)Enum.Parse(typeof(AlumnoEstado), estado)
                );


            modelBuilder.Entity<Matricula>()
                .HasOne(m => m.Alumno)
                .WithMany(a => a.Matriculas)
                .HasForeignKey(m => m.AlumnoId);

            modelBuilder.Entity<Matricula>()
                .HasOne(m => m.Curso)
                .WithMany(c => c.Matriculas)
                .HasForeignKey(m => m.CursoId);






            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                var result = await base.SaveChangesAsync(cancellationToken);
                await PublishDomainEventsAsync();
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

        private async Task PublishDomainEventsAsync()
        {


            var domainEvents = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.GetDomainEvents();
                entity.ClearDomainEvents();
                return domainEvents;
            }).ToList();

            foreach (var domainEvent in domainEvents)
            {

                var wrapperType = typeof(DomainEventNotification<>)
                .MakeGenericType(domainEvent.GetType());

                var notification = Activator.CreateInstance(wrapperType, domainEvent);

                if (notification != null)
                {
                    await _publisher.Publish(notification);
                }
            }





        }




    }
}
