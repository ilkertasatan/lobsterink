namespace LobsterInk.Adventure.Domain.UserAdventures.Services;

public interface IUserAdventureFactory
{
    UserAdventure NewUserAdventure(Guid treeId, Guid userId, Guid nodeId);
}