using LobsterInk.Adventure.Application.UseCases.SaveUserAdventure;
using Microsoft.AspNetCore.Mvc;

namespace LobsterInk.Adventure.Api.UseCases.V1.SaveUserAdventure;

/// <summary>
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class UserAdventuresController : ControllerBase
{
    private readonly ISaveUserAdventureUseCase _useCase;
    private readonly SaveUserAdventurePresenter _presenter;

    /// <inheritdoc />
    public UserAdventuresController(ISaveUserAdventureUseCase useCase, SaveUserAdventurePresenter presenter)
    {
        _useCase = useCase;
        _presenter = presenter;
    }

    /// <summary>
    /// Saves the adventure that user made
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SaveAsync([FromBody]SaveUserAdventureRequest request)
    {
        await _useCase.ExecuteAsync(new SaveUserAdventureInput(
            request.TreeId,
            request.UserId,
            request.NodeId), _presenter);
        
        return _presenter.ViewModel;
    }
}