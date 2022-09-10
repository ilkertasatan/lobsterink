using LobsterInk.Adventure.Domain.Exceptions;

namespace LobsterInk.Adventure.Domain.AdventureTrees;

public class AdventureTreeNode : IEquatable<AdventureTreeNode>
{
    private readonly List<AdventureTreeNode> _nodes;

    private AdventureTreeNode()
    {
        _nodes = new List<AdventureTreeNode>();
    }

    internal AdventureTreeNode(Guid nodeId, string name, AdventureTreeNode? parent, Guid treeId) : this()
    {
        if (nodeId == Guid.Empty)
            throw new DomainValidationException("NodeId cannot be empty");

        if (string.IsNullOrWhiteSpace(name))
            throw new DomainValidationException("NodeName cannot be null or empty");

        if (treeId == Guid.Empty)
            throw new DomainValidationException("TreeId cannot be empty");
        
        NodeId = nodeId;
        Name = name;
        ParentNode = parent;
        ParentNodeId = parent?.NodeId ?? Guid.Empty;
        TreeId = treeId;
    }
    
    public Guid NodeId { get; }
    
    public string Name { get; }
    
    public AdventureTreeNode? ParentNode { get; }
    
    public Guid ParentNodeId { get; }
    
    public IReadOnlyCollection<AdventureTreeNode> Nodes => _nodes;

    public Guid TreeId { get; }

    public bool Selected { get; private set; }

    public AdventureTreeNode AddChildNode(Guid nodeId, string name)
    {
        var node = new AdventureTreeNode(nodeId, name, parent: this, TreeId);
        _nodes.Add(node);
        
        return node;
    }

    public bool IsParentNode() => ParentNodeId == Guid.Empty;

    public void MarkAsSelected() => Selected = true;

    public bool IsChildOf(Guid nodeId) => ParentNodeId == nodeId;


    #region IEquatable Member

    public bool Equals(AdventureTreeNode? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return NodeId.Equals(other.NodeId) && Name == other.Name &&
               Equals(ParentNode, other.ParentNode) && ParentNodeId.Equals(other.ParentNodeId) &&
               TreeId.Equals(other.TreeId) && Selected == other.Selected;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as AdventureTreeNode);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(NodeId, Name, ParentNode, ParentNodeId, TreeId, Selected);
    }

    #endregion
}