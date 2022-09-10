using LobsterInk.Application.Abstraction.UseCase;

namespace LobsterInk.Adventure.Application.UseCases.GetUserAdventure;

public sealed class GetUserAdventureOutput : IUseCaseOutput
{
    public GetUserAdventureOutput(
        Guid nodeId,
        Guid? parentNodeId,
        string name,
        bool selected,
        IEnumerable<GetUserAdventureOutput> nodes)
    {
        Id = nodeId;
        ParentId = parentNodeId;
        Name = name;
        Selected = selected;
        Nodes = nodes.ToList();
    }

    public Guid Id { get; }

    public Guid? ParentId { get; }

    public string Name { get; }

    public bool Selected { get; }

    public List<GetUserAdventureOutput> Nodes { get; }
}