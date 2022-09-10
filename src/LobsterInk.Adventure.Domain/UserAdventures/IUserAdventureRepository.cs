namespace LobsterInk.Adventure.Domain.UserAdventures;

public interface IUserAdventureRepository
{
    Task InsertAsync(UserAdventure userAdventure);
    
    Task<bool> ExistsAsync(Guid treeId);

    
    Task<IEnumerable<UserAdventure>> SelectByAsync(Guid treeId, Guid userId);
}