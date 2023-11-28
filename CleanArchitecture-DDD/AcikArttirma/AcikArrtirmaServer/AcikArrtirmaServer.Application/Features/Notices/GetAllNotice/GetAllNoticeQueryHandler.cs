using AcikArrtirmaServer.Domain.Notices;
using MediatR;

namespace AcikArrtirmaServer.Application.Features.Notices.GetAllNotice;

internal sealed class GetAllNoticeQueryHandler : IRequestHandler<GetAllNoticeQuery, List<Notice>>
{
    private readonly INoticeRepository _noticeRepostory;

    public GetAllNoticeQueryHandler(INoticeRepository noticeRepostory)
    {
        _noticeRepostory = noticeRepostory;
    }

    public async Task<List<Notice>> Handle(GetAllNoticeQuery request, CancellationToken cancellationToken)
    {
        return await _noticeRepostory.GetAllAsync(cancellationToken);
    }
}