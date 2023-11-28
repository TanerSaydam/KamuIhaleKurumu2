namespace CleanArchitecture.Entities.Abstractions;

public abstract class Entity
{
    public Entity()
    {
        Id = Ulid.NewUlid().ToString();
    }

    public string Id { get; set; }
}