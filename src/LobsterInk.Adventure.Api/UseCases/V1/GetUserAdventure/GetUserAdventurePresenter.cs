using LobsterInk.Adventure.Api.UseCases.V1.GetUserAdventure.Extensions;
using LobsterInk.Adventure.Application.UseCases.GetUserAdventure;
using Microsoft.AspNetCore.Mvc;

namespace LobsterInk.Adventure.Api.UseCases.V1.GetUserAdventure;

public sealed class GetUserAdventurePresenter : IGetUserAdventureOutput
{
    public IActionResult ViewModel { get; private set; }
    
    public void Success(IEnumerable<GetUserAdventureOutput> output)
    {
        ViewModel = new OkObjectResult(output.ToViewModel());
    }
    
    public void ObjectNotFound(string message)
    {
        ViewModel = new NotFoundObjectResult(message);
    }
}