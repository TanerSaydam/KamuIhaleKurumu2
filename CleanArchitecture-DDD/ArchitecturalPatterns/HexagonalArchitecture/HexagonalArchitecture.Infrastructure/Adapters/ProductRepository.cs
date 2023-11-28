using HexagonalArchitecture.Application.Models;
using HexagonalArchitecture.Application.Ports;
using HexagonalArchitecture.Domain.Entities;
using HexagonalArchitecture.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonalArchitecture.Infrastructure.Adapters;

internal sealed class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(Product product)
    {
        throw new NotImplementedException();
    }

    public List<ProductModel> GetAll()
    {
        throw new NotImplementedException();
    }
}