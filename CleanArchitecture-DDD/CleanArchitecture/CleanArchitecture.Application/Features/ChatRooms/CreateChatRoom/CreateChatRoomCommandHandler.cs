using CleanArchitecture.Entities.Models;
using CleanArchitecture.Entities.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Features.ChatRooms.CreateChatRoom;

internal sealed class CreateChatRoomCommandHandler : IRequestHandler<CreateChatRoomCommand, string>
{
    private readonly IChatRoomRepository _chatRoomRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateChatRoomCommandHandler(IChatRoomRepository chatRoomRepository, IUnitOfWork unitOfWork)
    {
        _chatRoomRepository = chatRoomRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(CreateChatRoomCommand request, CancellationToken cancellationToken)
    {
        var lastChatRoom = await _chatRoomRepository.GetAll().OrderBy(p => p.Id).LastOrDefaultAsync();
        int number = 1;

        if (lastChatRoom is not null)
        {
            number = lastChatRoom.Number + 1;
        }

        ChatRoom chatRoom = new()
        {
            Number = number,
            IsClose = false,
            Subject = request.Subject,
            UserId = request.UserId
        };

        await _chatRoomRepository.AddAsync(chatRoom, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return chatRoom.Id;
    }
}