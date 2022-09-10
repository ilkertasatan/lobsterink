using LobsterInk.Adventure.Application.UseCases.GetAdventureTree.Extensions;
using LobsterInk.Adventure.Application.UseCases.GetAdventureTree.Validators;
using LobsterInk.Adventure.Domain.AdventureTrees;
using LobsterInk.Application.Abstraction.Exceptions;

namespace LobsterInk.Adventure.Application.UseCases.GetAdventureTree;

public sealed class GetAdventureTreeUseCase : IGetAdventureTreeUseCase
{
    private readonly IAdventureTreeRepository _trees;
    private readonly IAdventureTreeNodeRepository _nodes;

    public GetAdventureTreeUseCase(
        IAdventureTreeRepository trees,
        IAdventureTreeNodeRepository nodes)
    {
        _trees = trees;
        _nodes = nodes;
    }

    public async Task ExecuteAsync(GetAdventureTreeInput input, IGetAdventureTreeOutput output)
    {
        var result = await new GetAdventureTreeInputValidator().ValidateAsync(input);
        if (!result.IsValid) throw new ApplicationValidationException(result.ToDictionary());

        var tree = await _trees.SelectByAsync(input.TreeId);
        if (!tree.Exists())
        {
            output.ObjectNotFound($"AdventureTree with Id:'{input.TreeId}' does not exist");
            return;
        }

        var nodes = await _nodes.SelectByAsync(input.TreeId);
        tree.Build(nodes.ToList());
        
        output.Success(tree.Nodes.ToOutput());
    }
}