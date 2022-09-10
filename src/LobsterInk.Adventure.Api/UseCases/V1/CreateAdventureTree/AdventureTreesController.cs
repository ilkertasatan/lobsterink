using LobsterInk.Adventure.Application.UseCases.CreateAdventureTree;
using Microsoft.AspNetCore.Mvc;

namespace LobsterInk.Adventure.Api.UseCases.V1.CreateAdventureTree;

/// <summary>
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class AdventureTreesController : ControllerBase
{
    private readonly ICreateAdventureTreeUseCase _useCase;
    private readonly CreateAdventureTreePresenter _presenter;

    /// <inheritdoc />
    public AdventureTreesController(ICreateAdventureTreeUseCase useCase, CreateAdventureTreePresenter presenter)
    {
        _useCase = useCase;
        _presenter = presenter;
    }

    /// <summary>
    /// Creates a new adventure for specific user
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync([FromBody]CreateAdventureTreeRequest request)
    {
        await _useCase.ExecuteAsync(new CreateAdventureTreeInput(request.Name, request.UserId), _presenter);
        return _presenter.ViewModel;
    }
}