using LobsterInk.Adventure.Application.UseCases.GetAdventureTree;
using Microsoft.AspNetCore.Mvc;

namespace LobsterInk.Adventure.Api.UseCases.V1.GetAdventureTree;

/// <summary>
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/{treeId:guid}")]
[ApiController]
public class AdventureTreesController : ControllerBase
{
    private readonly IGetAdventureTreeUseCase _useCase;
    private readonly GetAdventureTreePresenter _presenter;

    /// <inheritdoc />
    public AdventureTreesController(IGetAdventureTreeUseCase useCase, GetAdventureTreePresenter presenter)
    {
        _useCase = useCase;
        _presenter = presenter;
    }

    /// <summary>
    /// Gets the adventure by its id
    /// </summary>
    /// <param name="treeId"></param>
    /// <returns></returns>
    [HttpGet("nodes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAsync(Guid treeId)
    {
        await _useCase.ExecuteAsync(new GetAdventureTreeInput(treeId), _presenter);
        return _presenter.ViewModel;
    }
}