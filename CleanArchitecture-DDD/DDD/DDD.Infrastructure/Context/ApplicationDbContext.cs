using DDD.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace DDD.Infrastructure.Context;

internal sealed class ApplicationDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=TUGAY\\SQLEXPRESS;Initial Catalog=Test2Db;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .OwnsOne(p => p.Price, priceBuilder =>
            {
                priceBuilder.Property(prop => prop.Amount).HasColumnType("money");
                priceBuilder.Property(prop => prop.Amount).HasColumnName("Amount");
                priceBuilder.Property(p => p.Currency).HasConversion(
                    c => c.Code, code => Currency.FromCode(code));
            });
    }
}

//Money_Amount
//Money_Currency