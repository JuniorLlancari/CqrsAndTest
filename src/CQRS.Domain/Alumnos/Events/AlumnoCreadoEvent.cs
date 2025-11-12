using CQRS.Domain.Abstraccions;

namespace CQRS.Domain.Alumnos.Events;

public sealed record AlumnoCreadoEvent(Guid IdAlumno) : IDomainEvent;
