using NTierArchitecuture.DataAccess.Dtos;
using NTierArchitecuture.DataAccess.Entities;

namespace NTierArchitecuture.DataAccess.Repositories;

public interface IProductRepository
{
    Task AddAsync(Product product, CancellationToken cancellationToken = default);

    Task<List<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default);
}