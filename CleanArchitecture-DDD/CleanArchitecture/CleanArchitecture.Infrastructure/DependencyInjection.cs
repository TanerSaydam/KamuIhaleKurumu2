using CleanArchitecture.Entities.Repositories;
using CleanArchitecture.Infrastructure.Context;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        //services.AddDbContext<ApplicationDbContext>(opt =>
        //{
        //    opt.UseSqlServer(configuration.GetConnectionString("SqlServer"));
        //});

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
        services.AddScoped<IChatRoomDetailRepository, ChatRoomDetailRepository>();

        services.AddScoped<ApplicationDbContext>();
        services
            .AddScoped<IUnitOfWork>(
                cfr =>
                cfr.GetRequiredService<ApplicationDbContext>());

        return services;
    }
}