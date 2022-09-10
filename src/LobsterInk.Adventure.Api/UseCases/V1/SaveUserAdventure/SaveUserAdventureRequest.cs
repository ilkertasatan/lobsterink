namespace LobsterInk.Adventure.Api.UseCases.V1.SaveUserAdventure;

public sealed class SaveUserAdventureRequest
{
    public Guid TreeId { get; set; }
    
    public Guid UserId { get; set; }
    
    public Guid NodeId { get; set; }
}