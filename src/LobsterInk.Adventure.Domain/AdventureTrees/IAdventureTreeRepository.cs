namespace LobsterInk.Adventure.Domain.AdventureTrees;

public interface IAdventureTreeRepository
{
    Task InsertAsync(AdventureTree tree);
    
    Task<AdventureTree> SelectByAsync(Guid treeId);
}