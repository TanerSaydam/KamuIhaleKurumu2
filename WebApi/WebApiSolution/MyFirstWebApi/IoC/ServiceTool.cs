namespace MyFirstWebApi.IoC;

public static class ServiceTool
{
    public static IServiceProvider ServiceProvider { get; private set; }
    public static IServiceCollection CreateService(this IServiceCollection services)
    {
         ServiceProvider = services.BuildServiceProvider();
         return services;
    }
}
