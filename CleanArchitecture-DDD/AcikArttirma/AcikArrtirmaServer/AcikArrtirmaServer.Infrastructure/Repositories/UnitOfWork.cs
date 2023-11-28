using AcikArrtirmaServer.Domain;
using AcikArrtirmaServer.Domain.Abstractions;
using AcikArrtirmaServer.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcikArrtirmaServer.Infrastructure.Repositories;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly IDbContext _context;

    public UnitOfWork(IDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var response = await _context.SaveChangesAsync(cancellationToken);
        return response;
    }
}