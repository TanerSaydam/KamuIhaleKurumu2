namespace DDD.Domain.Abstractions;

public abstract class Entity : IEquatable<Entity>
{
    public Entity(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; private set; }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        if (obj is not Entity entity)
        {
            return false;
        }

        return entity.Id == Id;
    }

    public bool Equals(Entity? other)
    {
        if (other is null)
        {
            return false;
        }

        if (other.GetType() != GetType())
        {
            return false;
        }

        if (other is not Entity entity)
        {
            return false;
        }

        return entity.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static bool operator ==(Entity obj1, Entity obj2)
    {
        return obj1.Equals(obj2);
    }

    public static bool operator !=(Entity obj1, Entity obj2)
    {
        return !obj1.Equals(obj2);
    }
}