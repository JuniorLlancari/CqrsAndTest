using CQRS.Application.Abstractions.Events;
using CQRS.Domain.Entities.Alumnos.Events;
using MediatR;

namespace CQRS.Application.Handlers.Alumnos.EventHandlers.Domain
{

    public class AlumnoCreadoEventHandler : 
        INotificationHandler<DomainEventNotification<AlumnoCreadoEvent>>
    {
        public Task Handle(DomainEventNotification<AlumnoCreadoEvent> notification, CancellationToken cancellationToken)
        {
            var id = notification.DomainEvent.IdAlumno;
            Console.Write($"Desde AlumnoCreadoEvent! ID del alumno: {id}");
            return Task.CompletedTask;
        }
    }
    
}
