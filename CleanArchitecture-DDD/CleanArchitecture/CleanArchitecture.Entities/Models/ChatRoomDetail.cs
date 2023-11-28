using CleanArchitecture.Entities.Abstractions;

namespace CleanArchitecture.Entities.Models;

public sealed class ChatRoomDetail : Entity
{
    public string ChatRoomId { get; set; }
    public string UserId { get; set; }
    public string Message { get; set; }
    public DateTime CreatedDate { get; set; }
}