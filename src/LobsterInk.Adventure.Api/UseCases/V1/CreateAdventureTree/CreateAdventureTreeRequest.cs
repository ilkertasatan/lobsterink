namespace LobsterInk.Adventure.Api.UseCases.V1.CreateAdventureTree;

public sealed class CreateAdventureTreeRequest
{
    public string Name { get; set; }
    
    public Guid UserId { get; set; }
}