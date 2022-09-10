using LobsterInk.Application.Abstraction.UseCase;

namespace LobsterInk.Adventure.Application.UseCases.GetUserAdventure;

public sealed class GetUserAdventureInput : IUseCaseInput
{
    public GetUserAdventureInput(Guid treeId, Guid userId)
    {
        TreeId = treeId;
        UserId = userId;
    }

    public Guid UserId { get; }
    
    public Guid TreeId { get; }
}