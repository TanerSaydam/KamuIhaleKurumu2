using AcikArrtirmaServer.Domain.Notices;
using MediatR;

namespace AcikArrtirmaServer.Application.Features.Notices.AddNotice;
public sealed record AddNoticeCommand(
    string Title,
    string Description,
    Money StartingFee,
    int ExpireTime) : IRequest;
