using Microsoft.Extensions.DependencyInjection;
using AuthZ.Domain.Interfaces;
using AuthZ.Infrastructure.Persistence.Repositories;

namespace AuthZ.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddAuthZInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        return services;
    }
}