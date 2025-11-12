using CQRS.Domain.Alumnos.Events;
using MediatR;

namespace CQRS.Application.Alumnos.EventHandlers.Domain
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
