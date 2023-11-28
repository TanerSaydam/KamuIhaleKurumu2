using NTierArchitecuture.DataAccess.Dtos;
using NTierArchitecuture.DataAccess.Entities;
using NTierArchitecuture.DataAccess.Repositories;

namespace NTierArchitecuture.Business.Services;

internal sealed class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task AddAsync(ProductDto request, CancellationToken cancellationToken = default)
    {
        if (request.Name.Length < 3)
        {
            throw new ArgumentException("Product adı 3 karakterden küçük olamaz!");
        }

        //kalan iş kuralları

        Product product = new()
        {
            Id = request.Id,
            Name = request.Name,
            Price = request.Price
        };

        await _productRepository.AddAsync(product, cancellationToken);
    }

    public async Task<List<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _productRepository.GetAllAsync(cancellationToken);
    }
}