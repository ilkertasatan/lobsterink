using FluentValidation;
using LobsterInk.Adventure.Application.UseCases.CreateAdventureTree.Validators;

namespace LobsterInk.Adventure.Api.Extensions;

public static class ValidatorExtensions
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        AssemblyScanner
            .FindValidatorsInAssembly(typeof(CreateAdventureTreeInputValidator).Assembly)
            .ForEach(item => 
                services.AddScoped(item.InterfaceType, item.ValidatorType));

        return services;
    }
}