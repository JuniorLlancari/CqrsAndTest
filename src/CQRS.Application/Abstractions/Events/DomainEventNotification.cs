using CQRS.Domain.Abstraccions;
using MediatR;

namespace CQRS.Application.Abstractions.Events;

public class DomainEventNotification<TEvent> : INotification
        where TEvent : IDomainEvent
{
    public TEvent DomainEvent { get; }

    public DomainEventNotification(TEvent domainEvent)
    {
        DomainEvent = domainEvent;
    }
}
