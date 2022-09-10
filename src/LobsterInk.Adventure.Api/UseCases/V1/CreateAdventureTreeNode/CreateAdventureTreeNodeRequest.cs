namespace LobsterInk.Adventure.Api.UseCases.V1.CreateAdventureTreeNode;

public sealed class CreateAdventureTreeNodeRequest
{
    public string Name { get; set; }

    public IEnumerable<CreateAdventureTreeNodeRequest>? Nodes { get; set; }
}
