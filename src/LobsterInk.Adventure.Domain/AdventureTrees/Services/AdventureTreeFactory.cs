namespace LobsterInk.Adventure.Domain.AdventureTrees.Services;

public sealed class AdventureTreeFactory : IAdventureTreeFactory
{
    public AdventureTree NewTree(Guid treeId, string name, Guid userId)
    {
        return new AdventureTree(treeId, name, userId);
    }
}