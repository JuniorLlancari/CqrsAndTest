using CQRS.Domain.Entities.Alumnos.Events;
using MediatR;

namespace CQRS.Application.Handlers.Alumnos.EventHandlers.Domain
{
    internal class AlumnoCreadoEventHandler : INotificationHandler<AlumnoCreadoEvent>
    {
        public Task Handle(AlumnoCreadoEvent notification, CancellationToken cancellationToken)
        {
            Console.Write("Desde AlumnoCreadoEvent!");
            return Task.CompletedTask;
        }
    }
}
