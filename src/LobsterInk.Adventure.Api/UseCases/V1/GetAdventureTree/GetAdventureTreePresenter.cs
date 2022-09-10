using LobsterInk.Adventure.Api.UseCases.V1.GetAdventureTree.Extensions;
using LobsterInk.Adventure.Application.UseCases.GetAdventureTree;
using Microsoft.AspNetCore.Mvc;

namespace LobsterInk.Adventure.Api.UseCases.V1.GetAdventureTree;

public sealed class GetAdventureTreePresenter : IGetAdventureTreeOutput
{
    public IActionResult ViewModel { get; private set; }

    public void Success(IEnumerable<GetAdventureTreeOutput> output)
    {
        ViewModel = new OkObjectResult(output.ToViewModel());
    }

    public void ObjectNotFound(string message)
    {
        ViewModel = new NotFoundObjectResult(message);
    }
}