using LobsterInk.Adventure.Application.UseCases.GetUserAdventure;

namespace LobsterInk.Adventure.Api.UseCases.V1.GetUserAdventure.Extensions;

public static class GetUserAdventureExtensions
{
    public static IEnumerable<GetUserAdventureResponse> ToViewModel(this IEnumerable<GetUserAdventureOutput> output)
    {
        return output
            .Select(o => new GetUserAdventureResponse(
                o.Id,
                o.ParentId,
                o.Name,
                o.Selected,
                o.Nodes.ToViewModel()))
            .ToList();
    }
}