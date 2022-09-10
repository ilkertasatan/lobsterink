using LobsterInk.Application.Abstraction.UseCase;

namespace LobsterInk.Adventure.Application.UseCases.GetAdventureTree;

public sealed class GetAdventureTreeOutput : IUseCaseOutput
{
    public GetAdventureTreeOutput(
        Guid nodeId,
        Guid? parentNodeId,
        string name,
        IEnumerable<GetAdventureTreeOutput> nodes)
    {
        Id = nodeId;
        ParentId = parentNodeId;
        Name = name;
        Nodes = nodes.ToList();
    }

    public Guid Id { get; }

    public Guid? ParentId { get; }

    public string Name { get; }

    public List<GetAdventureTreeOutput> Nodes { get; }
}