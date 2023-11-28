using CleanArchitecture.Entities.Models;
using CleanArchitecture.Entities.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Features.ChatRooms.GetAllChatRoom;

internal sealed class GetAllChatRoomQueryHandler : IRequestHandler<GetAllChatRoomQuery, List<ChatRoom>>
{
    private readonly IChatRoomRepository _chatRoomRepository;

    public GetAllChatRoomQueryHandler(IChatRoomRepository chatRoomRepository)
    {
        _chatRoomRepository = chatRoomRepository;
    }

    public async Task<List<ChatRoom>> Handle(GetAllChatRoomQuery request, CancellationToken cancellationToken)
    {
        return await _chatRoomRepository.GetAll().ToListAsync(cancellationToken);
    }
}