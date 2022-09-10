using LobsterInk.Adventure.Application.UseCases.CreateAdventureTree;
using Microsoft.AspNetCore.Mvc;

namespace LobsterInk.Adventure.Api.UseCases.V1.CreateAdventureTree;

public sealed class CreateAdventureTreePresenter : ICreateAdventureTreeOutput
{
    public IActionResult ViewModel { get; private set; }
    
    public void Success(Guid output)
    {
        ViewModel = new CreatedResult($"api/v1/adventures/{output}", output);
    }

    public void ValidationError(string message)
    {
        ViewModel = new BadRequestObjectResult(message);
    }
}