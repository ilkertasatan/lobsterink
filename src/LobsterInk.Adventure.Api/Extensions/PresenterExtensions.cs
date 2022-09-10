using LobsterInk.Adventure.Api.UseCases.V1.CreateAdventureTree;
using LobsterInk.Adventure.Api.UseCases.V1.CreateAdventureTreeNode;
using LobsterInk.Adventure.Api.UseCases.V1.GetAdventureTree;
using LobsterInk.Adventure.Api.UseCases.V1.GetUserAdventure;
using LobsterInk.Adventure.Api.UseCases.V1.SaveUserAdventure;

namespace LobsterInk.Adventure.Api.Extensions;

public static class PresenterExtensions
{
    public static IServiceCollection AddPresenters(this IServiceCollection services)
    {
        services.AddScoped<CreateAdventureTreePresenter, CreateAdventureTreePresenter>();
        services.AddScoped<CreateAdventureTreeNodePresenter, CreateAdventureTreeNodePresenter>();
        services.AddScoped<GetAdventureTreePresenter, GetAdventureTreePresenter>();
        services.AddScoped<SaveUserAdventurePresenter, SaveUserAdventurePresenter>();
        services.AddScoped<GetUserAdventurePresenter, GetUserAdventurePresenter>();
        
        return services;
    }
}