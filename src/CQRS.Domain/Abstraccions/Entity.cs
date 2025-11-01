namespace CQRS.Domain.Abstraccions;

public abstract class Entity
{
    protected Entity(){}
    protected Entity(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; init; }
}
