using AcikArrtirmaServer.Domain.Abstractions;
using AcikArrtirmaServer.Domain.Notices;
using MediatR;

namespace AcikArrtirmaServer.Application.Features.Notices.AddNotice;

internal sealed class AddNoticeCommandHandler : IRequestHandler<AddNoticeCommand>
{
    private readonly INoticeRepository _noticeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddNoticeCommandHandler(INoticeRepository noticeRepository, IUnitOfWork unitOfWork)
    {
        _noticeRepository = noticeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(AddNoticeCommand request, CancellationToken cancellationToken)
    {
        Notice notice = new(
                    request.Title,
                    request.Description,
                    request.StartingFee,
                    request.ExpireTime);

        await _noticeRepository.AddAsync(notice, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}