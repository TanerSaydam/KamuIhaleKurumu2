using AcikArrtirmaServer.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace AcikArrtirmaServer.Domain;

public interface IDbContext : IUnitOfWork
{
    DbSet<T> Set<T>() where T : class;
}