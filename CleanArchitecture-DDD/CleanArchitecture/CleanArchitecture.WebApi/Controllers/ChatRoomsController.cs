using CleanArchitecture.Application.Features.ChatRooms.CreateChatRoom;
using CleanArchitecture.Application.Features.ChatRooms.GetAllChatRoom;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ChatRoomsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChatRoomsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> GetAll(GetAllChatRoomQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateChatRoomCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}