using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddMediatR(cfr =>
        {
            //cfr.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfr
            .RegisterServicesFromAssembly(
                        typeof(DependencyInjection).Assembly);
        });
        return services;
    }
}