using LobsterInk.Application.Abstraction.UseCase;

namespace LobsterInk.Adventure.Application.UseCases.CreateAdventureTree;

public sealed class CreateAdventureTreeInput : IUseCaseInput
{
    public CreateAdventureTreeInput(string name, Guid userId)
    {
        Name = name;
        UserId = userId;
    }

    public string Name { get; }

    public Guid UserId { get; }
}

public sealed class AdventureTreeNodeInput
{
    public AdventureTreeNodeInput(string name, IEnumerable<AdventureTreeNodeInput>? nodes)
    {
        Name = name;
        Nodes = nodes;
    }

    public string Name { get; }
    
    public IEnumerable<AdventureTreeNodeInput>? Nodes { get; }
}