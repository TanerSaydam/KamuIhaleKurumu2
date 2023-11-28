namespace MyFirstWebApi.Entities;

public sealed class Role
{
    public Role()
    {
        Id = Ulid.NewUlid().ToString();
    }
    public string Id { get; set; }
    public string Name { get; set; }
}
