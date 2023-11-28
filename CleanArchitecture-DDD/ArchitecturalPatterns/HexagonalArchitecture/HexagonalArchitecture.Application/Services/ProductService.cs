using HexagonalArchitecture.Application.Models;
using HexagonalArchitecture.Application.Ports;
using HexagonalArchitecture.Domain.Entities;

namespace HexagonalArchitecture.Application.Services;

public sealed class ProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public void Add(ProductModel request)
    {
        Product product = new()
        {
            Id = request.Id,
            Name = request.Name,
            Price = request.Price
        };

        _productRepository.Add(product);
    }
}