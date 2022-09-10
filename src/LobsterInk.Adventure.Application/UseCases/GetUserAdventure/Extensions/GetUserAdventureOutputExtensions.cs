using LobsterInk.Adventure.Domain.AdventureTrees;

namespace LobsterInk.Adventure.Application.UseCases.GetUserAdventure.Extensions;

public static class GetUserAdventureOutputExtensions
{
    public static IEnumerable<GetUserAdventureOutput> ToOutput(this IEnumerable<AdventureTreeNode> nodes)
    {
        return nodes.Select(n => new GetUserAdventureOutput(n.NodeId, n.ParentNode?.NodeId, n.Name, n.Selected, n.Nodes.ToOutput()));
    }
}