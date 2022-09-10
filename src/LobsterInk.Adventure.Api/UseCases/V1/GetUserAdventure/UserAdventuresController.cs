using LobsterInk.Adventure.Application.UseCases.GetUserAdventure;
using Microsoft.AspNetCore.Mvc;

namespace LobsterInk.Adventure.Api.UseCases.V1.GetUserAdventure;

/// <summary>
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class UserAdventuresController : ControllerBase
{
    private readonly IGetUserAdventureUseCase _useCase;
    private readonly GetUserAdventurePresenter _presenter;

    /// <inheritdoc />
    public UserAdventuresController(IGetUserAdventureUseCase useCase, GetUserAdventurePresenter presenter)
    {
        _useCase = useCase;
        _presenter = presenter;
    }

    /// <summary>
    /// Gets the adventure that user made
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="treeId"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAsync([FromQuery]Guid treeId, [FromQuery]Guid userId)
    {
        await _useCase.ExecuteAsync(new GetUserAdventureInput(treeId, userId), _presenter);
        return _presenter.ViewModel;
    }
}