using AcikArrtirmaServer.Domain;
using AcikArrtirmaServer.Domain.Notices;
using Microsoft.EntityFrameworkCore;

namespace AcikArrtirmaServer.Infrastructure.Repositories;

internal sealed class NoticeRepository : INoticeRepository
{
    private readonly DbSet<Notice> Notice;
    private readonly IDbContext _context;
    public NoticeRepository(IDbContext context)
    {
        _context = context;
        Notice = context.Set<Notice>();
    }

    public async Task AddAsync(Notice notice, CancellationToken cancellationToken = default)
    {
        //await Notice.AddAsync(notice, cancellationToken);
        await _context.Set<Notice>().AddAsync(notice, cancellationToken);
    }

    public async Task<List<Notice>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        //return await Notice
                //.OrderByDescending(p => p.CreatedDate)
                //.Where(p => p.IsCompleted == false)
                //.ToListAsync(cancellationToken);
        return await _context.Set<Notice>()
                .OrderByDescending(p => p.CreatedDate)
                .Where(p => p.IsCompleted == false)
                .ToListAsync(cancellationToken);
    }
}