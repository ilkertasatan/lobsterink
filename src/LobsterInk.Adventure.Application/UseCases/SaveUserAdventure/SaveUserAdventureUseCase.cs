using LobsterInk.Adventure.Application.UseCases.SaveUserAdventure.Validators;
using LobsterInk.Adventure.Domain.AdventureTrees;
using LobsterInk.Adventure.Domain.Exceptions;
using LobsterInk.Adventure.Domain.UserAdventures;
using LobsterInk.Adventure.Domain.UserAdventures.Services;
using LobsterInk.Application.Abstraction.Exceptions;
using LobsterInk.Application.Abstraction.Services;

namespace LobsterInk.Adventure.Application.UseCases.SaveUserAdventure;

public sealed class SaveUserAdventureUseCase : ISaveUserAdventureUseCase
{
    private readonly IUserAdventureFactory _factory;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAdventureTreeRepository _trees;
    private readonly IUserAdventureRepository _userAdventures;

    public SaveUserAdventureUseCase(
        IUserAdventureFactory factory,
        IUnitOfWork unitOfWork,
        IAdventureTreeRepository trees,
        IUserAdventureRepository userAdventures)
    {
        _factory = factory;
        _unitOfWork = unitOfWork;
        _trees = trees;
        _userAdventures = userAdventures;
    }

    public async Task ExecuteAsync(SaveUserAdventureInput input, ISaveUserAdventureOutput output)
    {
        var result = await new SaveUserAdventureInputValidator().ValidateAsync(input);
        if (!result.IsValid) throw new ApplicationValidationException(result.ToDictionary());
        
        var tree = await _trees.SelectByAsync(input.TreeId);
        if (!tree.Exists())
        {
            output.ObjectNotFound($"AdventureTree with Id:'{input.TreeId}' does not exist");
            return;
        }
        
        var nodeExists = await _userAdventures.ExistsAsync(input.NodeId);
        if (nodeExists)
        {
            output.Success();
            return;
        }
        
        try
        {
            var userAdventure = _factory.NewUserAdventure(input.TreeId, input.UserId, input.NodeId);
            
            await _userAdventures.InsertAsync(userAdventure);
            await _unitOfWork.SubmitChangesAsync();
            
            output.Success();
        }
        catch (DomainValidationException exception)
        {
            output.ValidationError(exception.Message);
        }
    }
}