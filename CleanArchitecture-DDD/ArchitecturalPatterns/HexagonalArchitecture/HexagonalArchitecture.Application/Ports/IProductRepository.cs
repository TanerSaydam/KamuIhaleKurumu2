using HexagonalArchitecture.Application.Models;
using HexagonalArchitecture.Domain.Entities;

namespace HexagonalArchitecture.Application.Ports;

public interface IProductRepository
{
    void Add(Product product);

    List<ProductModel> GetAll();
}