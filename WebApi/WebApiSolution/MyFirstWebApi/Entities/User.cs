namespace MyFirstWebApi.Entities;

public sealed class User
{
    public User()
    {
        Id = Ulid.NewUlid().ToString();
    }
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Provider { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpires { get; set; }
}
