using MediatR;

namespace CleanArchitecture.Application.Features.ChatRooms.CreateChatRoom;
public sealed record CreateChatRoomCommand(
    string UserId,
    string Subject) : IRequest<string>;