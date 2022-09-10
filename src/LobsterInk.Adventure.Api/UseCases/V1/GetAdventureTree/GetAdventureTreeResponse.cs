namespace LobsterInk.Adventure.Api.UseCases.V1.GetAdventureTree;

public sealed class GetAdventureTreeResponse
{
    public GetAdventureTreeResponse(Guid id, Guid? parentId, string name, IEnumerable<GetAdventureTreeResponse> nodes)
    {
        Id = id;
        ParentId = parentId;
        Name = name;
        Nodes = nodes;
    }

    public Guid Id { get; }
    
    public Guid? ParentId { get; }

    public string Name { get; }
    
    public IEnumerable<GetAdventureTreeResponse> Nodes { get; }
}