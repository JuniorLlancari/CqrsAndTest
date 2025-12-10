using CQRS.Domain.Abstraccions;

namespace CQRS.Domain.Entities.Alumnos.Events;

public sealed record AlumnoCreadoEvent(Guid IdAlumno) : IDomainEvent;
