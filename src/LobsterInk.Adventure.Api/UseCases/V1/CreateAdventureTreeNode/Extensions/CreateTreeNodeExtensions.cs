using LobsterInk.Adventure.Application.UseCases.CreateAdventureTreeNode;

namespace LobsterInk.Adventure.Api.UseCases.V1.CreateAdventureTreeNode.Extensions;

public static class CreateTreeNodeExtensions
{
    public static IEnumerable<AdventureTreeNodeInput> ToInput(this IEnumerable<CreateAdventureTreeNodeRequest> nodes)
    {
        return nodes.Select(n => new AdventureTreeNodeInput(n.Name, (n.Nodes ?? Array.Empty<CreateAdventureTreeNodeRequest>()).ToInput()));
    }
}