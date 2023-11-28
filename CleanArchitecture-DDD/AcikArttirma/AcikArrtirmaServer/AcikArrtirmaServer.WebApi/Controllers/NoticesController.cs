using AcikArrtirmaServer.Application.Features.Notices.AddNotice;
using AcikArrtirmaServer.Application.Features.Notices.GetAllNotice;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AcikArrtirmaServer.WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class NoticesController : ControllerBase
{
    private readonly IMediator _mediator;

    public NoticesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> GetAll(GetAllNoticeQuery request, CancellationToken
         cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddNoticeCommand request, CancellationToken
        cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return NoContent();
    }
}