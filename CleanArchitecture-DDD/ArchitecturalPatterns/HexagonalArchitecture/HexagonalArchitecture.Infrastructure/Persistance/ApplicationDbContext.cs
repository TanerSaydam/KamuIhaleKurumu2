using HexagonalArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HexagonalArchitecture.Infrastructure.Persistance;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
}