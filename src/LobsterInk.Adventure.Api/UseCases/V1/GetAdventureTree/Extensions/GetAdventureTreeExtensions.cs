using LobsterInk.Adventure.Application.UseCases.GetAdventureTree;

namespace LobsterInk.Adventure.Api.UseCases.V1.GetAdventureTree.Extensions;

public static class GetAdventureTreeExtensions
{
    public static List<GetAdventureTreeResponse> ToViewModel(this IEnumerable<GetAdventureTreeOutput> output)
    {
        return output
            .Select(o =>
                new GetAdventureTreeResponse(
                    o.Id,
                    o.ParentId,
                    o.Name,
                    o.Nodes.ToViewModel()))
            .ToList();
    }
}