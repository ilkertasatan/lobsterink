using LobsterInk.Adventure.Application.UseCases.CreateAdventureTree;
using LobsterInk.Adventure.Application.UseCases.CreateAdventureTreeNode;
using LobsterInk.Adventure.Application.UseCases.GetAdventureTree;
using LobsterInk.Adventure.Application.UseCases.GetUserAdventure;
using LobsterInk.Adventure.Application.UseCases.SaveUserAdventure;

namespace LobsterInk.Adventure.Api.Extensions;

public static class UseCaseExtensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<ICreateAdventureTreeUseCase, CreateAdventureTreeUseCase>();
        services.AddScoped<ICreateAdventureTreeNodeUseCase, CreateAdventureTreeNodeUseCase>();
        services.AddScoped<IGetAdventureTreeUseCase, GetAdventureTreeUseCase>();
        services.AddScoped<ISaveUserAdventureUseCase, SaveUserAdventureUseCase>();
        services.AddScoped<IGetUserAdventureUseCase, GetUserAdventureUseCase>();
        
        return services;
    }
}