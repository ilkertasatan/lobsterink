using LobsterInk.Adventure.Application.UseCases.CreateAdventureTree.Validators;
using LobsterInk.Adventure.Domain.AdventureTrees;
using LobsterInk.Adventure.Domain.AdventureTrees.Services;
using LobsterInk.Adventure.Domain.Exceptions;
using LobsterInk.Application.Abstraction.Exceptions;
using LobsterInk.Application.Abstraction.Services;

namespace LobsterInk.Adventure.Application.UseCases.CreateAdventureTree;

public sealed class CreateAdventureTreeUseCase : ICreateAdventureTreeUseCase
{
    private readonly IAdventureTreeFactory _treeFactory;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAdventureTreeRepository _trees;
    private readonly ISequentialGuid _guid;

    public CreateAdventureTreeUseCase(
        IAdventureTreeFactory treeFactory,
        IUnitOfWork unitOfWork,
        IAdventureTreeRepository trees,
        ISequentialGuid guid)
    {
        _treeFactory = treeFactory;
        _unitOfWork = unitOfWork;
        _trees = trees;
        _guid = guid;
    }

    public async Task ExecuteAsync(CreateAdventureTreeInput input, ICreateAdventureTreeOutput output)
    {
        var result = await new CreateAdventureTreeInputValidator().ValidateAsync(input);
        if (!result.IsValid) throw new ApplicationValidationException(result.ToDictionary());

        try
        {
            var tree = _treeFactory.NewTree(_guid.NewGuid(), input.Name, input.UserId);

            await _trees.InsertAsync(tree);
            await _unitOfWork.SubmitChangesAsync();

            output.Success(tree.TreeId);
        }
        catch (DomainValidationException exception)
        {
            output.ValidationError(exception.Message);
        }
    }
}