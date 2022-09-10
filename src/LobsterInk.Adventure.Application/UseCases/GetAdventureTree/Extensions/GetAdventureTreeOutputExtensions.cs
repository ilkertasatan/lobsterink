using LobsterInk.Adventure.Domain.AdventureTrees;

namespace LobsterInk.Adventure.Application.UseCases.GetAdventureTree.Extensions;

public static class GetAdventureTreeOutputExtensions
{
    public static IEnumerable<GetAdventureTreeOutput> ToOutput(this IEnumerable<AdventureTreeNode> nodes)
    {
        return nodes.Select(n => new GetAdventureTreeOutput(n.NodeId, n.ParentNode?.NodeId, n.Name, n.Nodes.ToOutput()));
    }
}