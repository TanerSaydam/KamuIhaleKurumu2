using CleanArchitecture.Entities.Models;
using CleanArchitecture.Entities.Repositories;
using CleanArchitecture.Infrastructure.Context;

namespace CleanArchitecture.Infrastructure.Repositories;

internal sealed class ChatRoomRepository : Repository<ChatRoom>, IChatRoomRepository
{
    public ChatRoomRepository(ApplicationDbContext context) : base(context)
    {
    }
}
