using LobsterInk.Adventure.Domain.AdventureTrees.Services;
using LobsterInk.Adventure.Domain.UserAdventures.Services;

namespace LobsterInk.Adventure.Api.Extensions;

public static class DomainServiceExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IAdventureTreeFactory, AdventureTreeFactory>();
        services.AddScoped<IUserAdventureFactory, UserAdventureFactory>();
        
        return services;
    }
}