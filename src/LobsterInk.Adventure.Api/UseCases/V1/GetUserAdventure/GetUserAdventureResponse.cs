namespace LobsterInk.Adventure.Api.UseCases.V1.GetUserAdventure;

public sealed class GetUserAdventureResponse
{
    public GetUserAdventureResponse(Guid id, Guid? parentId, string name, bool selected, IEnumerable<GetUserAdventureResponse> nodes)
    {
        Id = id;
        ParentId = parentId;
        Name = name;
        Nodes = nodes;
        Selected = selected;
    }

    public Guid Id { get; }
    
    public Guid? ParentId { get; }

    public string Name { get; }

    public bool Selected { get; }
    
    public IEnumerable<GetUserAdventureResponse> Nodes { get; }
}