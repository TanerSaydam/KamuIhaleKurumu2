using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersonelServer.Domain.Abstractions;
using PersonelServer.Domain.Users;
using PersonelServer.Infrastructure.Context;
using PersonelServer.Infrastructure.Repositories;

namespace PersonelServer.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUnitOfWork>(srv=> srv.GetRequiredService<ApplicationDbContext>());

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer("Data Source=TUGAY\\SQLEXPRESS;Initial Catalog=PersonelDb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        });

        return services;
    }
}
