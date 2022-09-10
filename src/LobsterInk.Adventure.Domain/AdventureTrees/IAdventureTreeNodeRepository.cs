namespace LobsterInk.Adventure.Domain.AdventureTrees;

public interface IAdventureTreeNodeRepository
{
    Task InsertAsync(AdventureTreeNode node);
    
    Task<IEnumerable<AdventureTreeNode>> SelectByAsync(Guid treeId);
}