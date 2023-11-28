namespace AcikArrtirmaServer.Domain.Notices;

public interface INoticeRepository
{
    Task AddAsync(Notice notice, CancellationToken cancellationToken = default);

    Task<List<Notice>> GetAllAsync(CancellationToken cancellationToken = default);
}