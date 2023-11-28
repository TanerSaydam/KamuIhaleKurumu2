using CleanArchitecture.Entities.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Context;

internal sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    //private readonly IConfiguration _configuration;

    //public ApplicationDbContext(IConfiguration configuration)
    //{
    //    _configuration = configuration;
    //}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=TUGAY\\SQLEXPRESS;Initial Catalog=TestCleanArchitectureKIK2Db;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
    }

    //public ApplicationDbContext(DbContextOptions options) : base(options)
    //{
    //}

    //public DbSet<User> Users { get; set; }
    //public DbSet<ChatRoom> ChatRooms { get; set; }
    //public DbSet<ChatRoomDetail> ChatRoomDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly);
    }
}