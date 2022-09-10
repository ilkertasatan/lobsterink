using LobsterInk.Application.Abstraction.UseCase;

namespace LobsterInk.Adventure.Application.UseCases.SaveUserAdventure;

public sealed class SaveUserAdventureInput : IUseCaseInput
{
    public SaveUserAdventureInput(Guid treeId, Guid userId, Guid nodeId)
    {
        TreeId = treeId;
        UserId = userId;
        NodeId = nodeId;
    }

    public Guid TreeId { get; }
    
    public Guid UserId { get; }

    public Guid NodeId { get; }
}