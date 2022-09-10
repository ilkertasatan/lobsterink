using LobsterInk.Application.Abstraction.UseCase;

namespace LobsterInk.Adventure.Application.UseCases.CreateAdventureTreeNode;

public sealed class CreateAdventureTreeNodeInput : IUseCaseInput
{
    public CreateAdventureTreeNodeInput(Guid treeId, IEnumerable<AdventureTreeNodeInput> inputs)
    {
        TreeId = treeId;
        Nodes = inputs;
    }

    public Guid TreeId { get; }
    
    public IEnumerable<AdventureTreeNodeInput> Nodes { get; }
}


public sealed class AdventureTreeNodeInput
{
    public AdventureTreeNodeInput(string name, IEnumerable<AdventureTreeNodeInput> nodes)
    {
        Name = name;
        Nodes = nodes;
    }

    public string Name { get; }

    public IEnumerable<AdventureTreeNodeInput> Nodes { get; }  
}