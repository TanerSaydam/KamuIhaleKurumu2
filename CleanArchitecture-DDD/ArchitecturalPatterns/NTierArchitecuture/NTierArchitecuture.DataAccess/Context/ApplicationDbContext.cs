using Microsoft.EntityFrameworkCore;
using NTierArchitecuture.DataAccess.Entities;

namespace NTierArchitecuture.DataAccess.Context;

internal sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
}