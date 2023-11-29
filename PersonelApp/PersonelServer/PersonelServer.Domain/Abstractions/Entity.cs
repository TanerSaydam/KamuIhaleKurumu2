namespace PersonelServer.Domain.Abstractions;
public abstract class Entity : IEquatable<Entity>
{
    public Entity()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; init; }

    public override bool Equals(object? obj)
    {
        if(obj is null || obj is not Entity entity) return false;

        return entity.Id == Id;
    }

    public bool Equals(Entity? other)
    {
        if (other is null || other is not Entity entity) return false;

        return entity.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
