using Auth.Application.Interfaces;
using Auth.Application.UseCases.RegisterUser;
using Auth.Domain.Interfaces;
using Auth.Infrastructure.Persistence.Repositories;
using Auth.Infrastructure.Services;

namespace Auth.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthModule(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<RegisterUserHandler>();

        return services;
    }
}