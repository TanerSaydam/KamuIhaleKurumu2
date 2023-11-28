using CleanArchitecture.Entities.Abstractions;

namespace CleanArchitecture.Entities.Models;

public sealed class ChatRoom : Entity
{
    public int Number { get; set; }
    public string UserId { get; set; }
    public string Subject { get; set; }
    public bool IsClose { get; set; } = false;
}