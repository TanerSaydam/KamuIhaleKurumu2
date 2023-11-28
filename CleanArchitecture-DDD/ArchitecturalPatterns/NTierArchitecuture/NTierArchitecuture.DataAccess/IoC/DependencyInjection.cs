using Microsoft.Extensions.DependencyInjection;
using NTierArchitecuture.DataAccess.Context;
using NTierArchitecuture.DataAccess.Repositories;

namespace NTierArchitecuture.DataAccess.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>();
        services.AddScoped<IProductRepository, ProductRepository>();
        return services;
    }
}