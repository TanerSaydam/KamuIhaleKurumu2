using AcikArrtirmaServer.Domain;
using AcikArrtirmaServer.Domain.Abstractions;
using AcikArrtirmaServer.Domain.Notices;
using AcikArrtirmaServer.Infrastructure.Context;
using AcikArrtirmaServer.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AcikArrtirmaServer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IDbContext, ApplicationDbContext>();
        services.AddScoped<INoticeRepository, NoticeRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}