using CleanArchitecture.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Entities.Repositories;

public interface IRepository<TEntity>
    where TEntity : Entity
{
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    void Update(TEntity entity);

    void Remove(TEntity entity);

    IQueryable<TEntity> GetAll();

    IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> expression);

    Task<TEntity> GetByIdAsync(string id, CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(
            Expression<Func<TEntity, bool>> expression,
            CancellationToken cancellationToken = default);
}