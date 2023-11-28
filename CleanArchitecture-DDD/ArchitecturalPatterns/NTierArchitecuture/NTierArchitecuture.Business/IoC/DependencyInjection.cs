using Microsoft.Extensions.DependencyInjection;
using NTierArchitecuture.Business.Services;

namespace NTierArchitecuture.Business.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddBusiness(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        return services;
    }
}