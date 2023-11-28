using CleanArchitecture.Entities.Models;
using CleanArchitecture.Entities.Repositories;
using CleanArchitecture.Infrastructure.Context;

namespace CleanArchitecture.Infrastructure.Repositories;

internal sealed class ChatRoomDetailRepository : Repository<ChatRoomDetail>, IChatRoomDetailRepository
{
    public ChatRoomDetailRepository(ApplicationDbContext context) : base(context)
    {
    }
}