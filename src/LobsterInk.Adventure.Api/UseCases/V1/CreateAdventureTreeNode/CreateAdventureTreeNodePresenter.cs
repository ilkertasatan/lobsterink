using LobsterInk.Adventure.Application.UseCases.CreateAdventureTreeNode;
using Microsoft.AspNetCore.Mvc;

namespace LobsterInk.Adventure.Api.UseCases.V1.CreateAdventureTreeNode;

public sealed class CreateAdventureTreeNodePresenter : ICreateAdventureTreeNodeOutput
{
    public IActionResult ViewModel { get; private set; }
    
    public void Success()
    {
        ViewModel = new CreatedResult(string.Empty, null);
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