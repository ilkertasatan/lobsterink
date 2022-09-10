using LobsterInk.Adventure.Application.UseCases.GetUserAdventure.Extensions;
using LobsterInk.Adventure.Application.UseCases.GetUserAdventure.Validators;
using LobsterInk.Adventure.Domain.AdventureTrees;
using LobsterInk.Adventure.Domain.UserAdventures;
using LobsterInk.Application.Abstraction.Exceptions;

namespace LobsterInk.Adventure.Application.UseCases.GetUserAdventure;

public sealed class GetUserAdventureUseCase : IGetUserAdventureUseCase
{
    private readonly IAdventureTreeRepository _trees;
    private readonly IAdventureTreeNodeRepository _nodes;
    private readonly IUserAdventureRepository _userAdventures;

    public GetUserAdventureUseCase(
        IAdventureTreeRepository trees,
        IAdventureTreeNodeRepository nodes, 
        IUserAdventureRepository userAdventures)
    {
        _trees = trees;
        _nodes = nodes;
        _userAdventures = userAdventures;
    }

    public async Task ExecuteAsync(GetUserAdventureInput input, IGetUserAdventureOutput output)
    {
        var result = await new GetUserAdventureInputValidator().ValidateAsync(input);
        if (!result.IsValid) throw new ApplicationValidationException(result.ToDictionary());
        
        var tree = await _trees.SelectByAsync(input.TreeId);
        if (!tree.Exists())
        {
            output.ObjectNotFound($"AdventureTree with Id:'{input.TreeId}' does not exist");
            return;
        }

        var nodes = (await _nodes.SelectByAsync(input.TreeId)).ToList();
        var userChoices = (await _userAdventures.SelectByAsync(input.TreeId, input.UserId)).ToList();

        tree.Build(nodes)
            .AddUserChoices(userChoices)
            .MarkAsSelected();
        
        output.Success(tree.Nodes.ToOutput());
    }
}