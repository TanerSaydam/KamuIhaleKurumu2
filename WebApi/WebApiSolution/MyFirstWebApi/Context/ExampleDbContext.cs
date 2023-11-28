using Microsoft.EntityFrameworkCore;
using MyFirstWebApi.IoC;

namespace MyFirstWebApi.Conctext;

public class ExampleDbContext : DbContext
{
    //private readonly IConfiguration _configuration;

    //public AppDbContext(IConfiguration configuration)
    //{
    //    _configuration = configuration;
    //}

    public ExampleDbContext()
    {
        //var configuration = ServiceTool.ServiceProvider.GetRequiredService<IConfiguration>();
        //string connectionString = configuration.GetSection("SqlServer").Value;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = ServiceTool.ServiceProvider.GetRequiredService<IConfiguration>();
        optionsBuilder.UseSqlServer(configuration.GetSection("SqlServer").Value);
    }
    //public AppDbContext(DbContextOptions options) : base(options)
    //{
    //}
}
