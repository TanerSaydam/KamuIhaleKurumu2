using CleanArchitecture.Entities.Models;
using MediatR;

namespace CleanArchitecture.Application.Features.ChatRooms.GetAllChatRoom;
public sealed record GetAllChatRoomQuery() : IRequest<List<ChatRoom>>;