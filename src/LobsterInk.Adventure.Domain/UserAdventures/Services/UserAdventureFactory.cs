namespace LobsterInk.Adventure.Domain.UserAdventures.Services;

public sealed class UserAdventureFactory : IUserAdventureFactory
{
    public UserAdventure NewUserAdventure(Guid treeId, Guid userId, Guid nodeId)
    {
        return new UserAdventure(treeId, userId, nodeId);
    }
}