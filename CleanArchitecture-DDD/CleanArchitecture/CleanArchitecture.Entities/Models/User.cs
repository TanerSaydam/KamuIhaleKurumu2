using CleanArchitecture.Entities.Abstractions;

namespace CleanArchitecture.Entities.Models;

public sealed class User : Entity
{
    public string Name { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}