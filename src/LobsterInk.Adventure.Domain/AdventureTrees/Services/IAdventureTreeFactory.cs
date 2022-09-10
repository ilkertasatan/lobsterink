namespace LobsterInk.Adventure.Domain.AdventureTrees.Services;

public interface IAdventureTreeFactory
{
    AdventureTree NewTree(Guid treeId, string name, Guid userId);
}