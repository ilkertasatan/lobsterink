using LobsterInk.Adventure.Application.UseCases.SaveUserAdventure;
using Microsoft.AspNetCore.Mvc;

namespace LobsterInk.Adventure.Api.UseCases.V1.SaveUserAdventure;

public sealed class SaveUserAdventurePresenter : ISaveUserAdventureOutput
{
    public IActionResult ViewModel { get; private set; }
    
    public void Success()
    {
        ViewModel = new CreatedResult("", null);
    }

    public void ValidationError(string message)
    {
        ViewModel = new BadRequestObjectResult(message);
    }

    public void ObjectNotFound(string message)
    {
        ViewModel = new NotFoundObjectResult(message);
    }
}