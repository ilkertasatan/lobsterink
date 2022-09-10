using LobsterInk.Application.Abstraction.UseCase;

namespace LobsterInk.Adventure.Application.UseCases.GetAdventureTree;

public sealed class GetAdventureTreeInput : IUseCaseInput
{
    public GetAdventureTreeInput(Guid treeId)
    {
        TreeId = treeId;
    }

    public Guid TreeId { get; }
}