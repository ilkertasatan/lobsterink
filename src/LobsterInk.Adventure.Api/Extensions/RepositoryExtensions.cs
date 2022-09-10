using LobsterInk.Adventure.Domain.AdventureTrees;
using LobsterInk.Adventure.Domain.UserAdventures;
using LobsterInk.Adventure.Infrastructure.DataAccess;
using LobsterInk.Adventure.Infrastructure.DataAccess.Repositories;
using LobsterInk.Application.Abstraction.Services;

namespace LobsterInk.Adventure.Api.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAdventureTreeRepository, AdventureTreeRepository>();
        services.AddScoped<IAdventureTreeNodeRepository, AdventureTreeNodeRepository>();
        services.AddScoped<IUserAdventureRepository, UserAdventureRepository>();
        
        return services;
    }
}