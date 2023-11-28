using Microsoft.EntityFrameworkCore;
using NTierArchitecuture.DataAccess.Context;
using NTierArchitecuture.DataAccess.Dtos;
using NTierArchitecuture.DataAccess.Entities;

namespace NTierArchitecuture.DataAccess.Repositories;

internal sealed class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _context.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Products.Select(s => new ProductDto()
        {
            Id = s.Id,
            Name = s.Name,
            Price = s.Price
        }).ToListAsync(cancellationToken);
    }
}