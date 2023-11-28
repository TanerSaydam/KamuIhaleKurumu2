using AcikArrtirmaServer.Domain.Notices;
using MediatR;

namespace AcikArrtirmaServer.Application.Features.Notices.GetAllNotice;
public sealed record GetAllNoticeQuery() : IRequest<List<Notice>>;