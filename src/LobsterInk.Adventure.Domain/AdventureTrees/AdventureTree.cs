using LobsterInk.Adventure.Domain.Exceptions;
using LobsterInk.Adventure.Domain.UserAdventures;

namespace LobsterInk.Adventure.Domain.AdventureTrees;

public class AdventureTree : AggregateRoot, IMaybeExist, IEquatable<AdventureTree>
{
    public static readonly AdventureTree Empty = new();
    
    private readonly List<AdventureTreeNode> _nodes;
    
    private AdventureTree()
    {
        _nodes = new List<AdventureTreeNode>();
    }

    internal AdventureTree(Guid treeId, string name, Guid userId) : this()
    {
        if (treeId == Guid.Empty)
            throw new DomainValidationException("TreeId cannot be empty");
        
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainValidationException("Name cannot be null or empty");
        
        if (userId == Guid.Empty)
            throw new DomainValidationException("UserId cannot be empty");
        
        TreeId = treeId;
        Name = name;
        UserId = userId;
    }

    public Guid TreeId { get; }

    public string Name { get; }

    public Guid UserId { get; }
    
    public IReadOnlyCollection<AdventureTreeNode> Nodes => _nodes;
    
    public DateTime CreatedOn { get; }

    public IReadOnlyCollection<UserAdventure> UserChoices { get; private set; }

    public bool Exists() => TreeId != Guid.Empty;

    public AdventureTree AddUserChoices(List<UserAdventure> choices)
    {
        UserChoices = choices;
        return this;
    }

    public void MarkAsSelected()
    {
        foreach (var node in Nodes)
        {
            MarkAsSelectedIfInUserChoices(node);
            MarkAsSelectedNextNode(node);
        }
    }

    public AdventureTreeNode AddParentNode(Guid nodeId, string name)
    {
        var node = new AdventureTreeNode(nodeId, name, parent: null, TreeId);
        _nodes.Add(node);

        return node;
    }
    
    public AdventureTree Build(List<AdventureTreeNode> nodes)
    {
        foreach (var parentNode in nodes.Where(n => n.IsParentNode()))
        {
            var node = AddParentNode(parentNode.NodeId, parentNode.Name);
            AddNodes(node, nodes);
        }

        return this;
    }

    private static void AddNodes(AdventureTreeNode parentNode, List<AdventureTreeNode> nodes)
    {
        foreach (var childNode in nodes.Where(n => n.IsChildOf(parentNode.NodeId)))
        {
            var node = parentNode.AddChildNode(childNode.NodeId, childNode.Name);
            AddNodes(node, nodes);
        }
    }

    private void MarkAsSelectedNextNode(AdventureTreeNode parentNode)
    {
        foreach (var node in parentNode.Nodes)
        {
            MarkAsSelectedIfInUserChoices(node);
            MarkAsSelectedNextNode(node);
        }
    }

    private void MarkAsSelectedIfInUserChoices(AdventureTreeNode node)
    {
        var userChoice = UserChoices.Any(c => c.IsSelected(node.NodeId));
        if (userChoice) node.MarkAsSelected(); 
    }
    
    #region IEquatable Members
    
    public bool Equals(AdventureTree? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return TreeId.Equals(other.TreeId) && Name == other.Name && UserId.Equals(other.UserId);
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as AdventureTree);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(TreeId, Name, UserId);
    }
    
    #endregion
}