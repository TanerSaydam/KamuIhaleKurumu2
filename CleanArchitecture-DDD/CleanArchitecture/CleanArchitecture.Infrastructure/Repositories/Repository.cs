using CleanArchitecture.Entities.Abstractions;
using CleanArchitecture.Entities.Repositories;
using CleanArchitecture.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CleanArchitecture.Infrastructure.Repositories;

internal class Repository<TEntity> : IRepository<TEntity>
    where TEntity : Entity
{
    private readonly ApplicationDbContext _context;
    private DbSet<TEntity> Entity;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        Entity = _context.Set<TEntity>();
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Entity.AddAsync(entity, cancellationToken);
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await Entity.AnyAsync(expression, cancellationToken);
    }

    public IQueryable<TEntity> GetAll()
    {
        return Entity.AsNoTracking().AsQueryable();
    }

    public async Task<TEntity> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await Entity.FindAsync(id, cancellationToken);
    }

    public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> expression)
    {
        return Entity.Where(expression).AsNoTracking().AsQueryable();
    }

    public void Remove(TEntity entity)
    {
        Entity.Remove(entity);
    }

    public void Update(TEntity entity)
    {
        Entity.Update(entity);
    }
}