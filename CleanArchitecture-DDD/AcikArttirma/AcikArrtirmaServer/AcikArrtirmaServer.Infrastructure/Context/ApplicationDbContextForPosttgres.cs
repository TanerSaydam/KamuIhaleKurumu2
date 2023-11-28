using AcikArrtirmaServer.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AcikArrtirmaServer.Infrastructure.Context;

internal class ApplicationDbContextForPosttgres : DbContext, IDbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=admin;Database=TestDb;").UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}