using LobsterInk.Adventure.Api.UseCases.V1.CreateAdventureTreeNode.Extensions;
using LobsterInk.Adventure.Application.UseCases.CreateAdventureTreeNode;
using Microsoft.AspNetCore.Mvc;

namespace LobsterInk.Adventure.Api.UseCases.V1.CreateAdventureTreeNode;

/// <summary>
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/{treeId:guid}")]
[ApiController]
public class AdventureTreesController : ControllerBase
{
    private readonly ICreateAdventureTreeNodeUseCase _useCase;
    private readonly CreateAdventureTreeNodePresenter _presenter;

    /// <inheritdoc />
    public AdventureTreesController(ICreateAdventureTreeNodeUseCase useCase, CreateAdventureTreeNodePresenter presenter)
    {
        _useCase = useCase;
        _presenter = presenter;
    }

    /// <summary>
    /// Creates a new adventure node for given adventure tree
    /// </summary>
    /// <param name="treeId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("nodes")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync(
        [FromRoute] Guid treeId,
        [FromBody] IEnumerable<CreateAdventureTreeNodeRequest> request)
    {
        await _useCase.ExecuteAsync(new CreateAdventureTreeNodeInput(treeId, request.ToInput()), _presenter);
        return _presenter.ViewModel;
    }
}