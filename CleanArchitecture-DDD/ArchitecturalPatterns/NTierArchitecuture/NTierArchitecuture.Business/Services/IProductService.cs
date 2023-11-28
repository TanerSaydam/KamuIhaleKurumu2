using NTierArchitecuture.DataAccess.Dtos;

namespace NTierArchitecuture.Business.Services;

public interface IProductService
{
    Task AddAsync(ProductDto request, CancellationToken cancellationToken = default);

    Task<List<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default);
}