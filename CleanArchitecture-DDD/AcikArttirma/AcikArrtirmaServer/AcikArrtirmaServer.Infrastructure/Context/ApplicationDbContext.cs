using AcikArrtirmaServer.Domain;
using AcikArrtirmaServer.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AcikArrtirmaServer.Infrastructure.Context;

internal sealed class ApplicationDbContext : DbContext, IDbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=TUGAY\\SQLEXPRESS;Initial Catalog=AcikArttirmaDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}