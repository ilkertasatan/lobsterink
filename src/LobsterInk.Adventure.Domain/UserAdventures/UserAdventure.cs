using LobsterInk.Adventure.Domain.Exceptions;

namespace LobsterInk.Adventure.Domain.UserAdventures;

public class UserAdventure : IEquatable<UserAdventure>
{
    private UserAdventure()
    {
    }
    
    internal UserAdventure(Guid treeId, Guid userId, Guid nodeId)
    {
        if (treeId == Guid.Empty)
            throw new DomainValidationException("TreeId cannot be empty");
        
        if (userId == Guid.Empty)
            throw new DomainValidationException("UserId cannot be empty");

        if (nodeId == Guid.Empty)
            throw new DomainValidationException("NodeId cannot be empty");
        
        TreeId = treeId;
        UserId = userId;
        NodeId = nodeId;
    }

    public Guid TreeId { get; }

    public Guid UserId { get; }

    public Guid NodeId { get; }

    public bool IsSelected(Guid nodeId) => NodeId == nodeId;

    #region IEquatable Member

    public bool Equals(UserAdventure? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return TreeId.Equals(other.TreeId) && UserId.Equals(other.UserId) && NodeId.Equals(other.NodeId);
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as UserAdventure);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(TreeId, UserId, NodeId);
    }

    #endregion
}