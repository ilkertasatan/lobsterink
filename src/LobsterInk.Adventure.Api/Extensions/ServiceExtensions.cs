using LobsterInk.Adventure.Infrastructure.Services;
using LobsterInk.Application.Abstraction.Services;

namespace LobsterInk.Adventure.Api.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ISequentialGuid, SequentialGuidService>();
        return services;
    }
}