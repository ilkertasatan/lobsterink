using FluentAssertions;
using LobsterInk.Adventure.Domain.AdventureTrees;
using LobsterInk.Adventure.Domain.AdventureTrees.Services;

namespace LobsterInk.Adventure.UnitTests.DomainTests;

public class AdventureTreeTests
{
    private readonly AdventureTree _tree;
    
    public AdventureTreeTests()
    {
        _tree = new AdventureTreeFactory().NewTree(treeId: Guid.NewGuid(), "test", userId: Guid.NewGuid());
    }
    
    [Fact]
    public void ShouldBe_ParentNode()
    {
        var sut = _tree.AddParentNode(nodeId: Guid.NewGuid(), "root");

        sut.IsParentNode().Should().BeTrue();
    }

    [Fact]
    public void Should_AddNode_To_Parent()
    {
        var expectedNodeId = Guid.NewGuid();
        const string expectedNodeName = "child of root";
        var sut = _tree.AddParentNode(nodeId: Guid.NewGuid(), "root");
        
        var actualResult = sut.AddChildNode(expectedNodeId, expectedNodeName);

        var actualNode = actualResult.Should().BeOfType<AdventureTreeNode>().Subject;
        actualNode.NodeId.Should().Be(expectedNodeId);
        actualNode.Name.Should().Be(expectedNodeName);
    }

    [Fact]
    public void ShouldBe_ChildOf_Given_Node()
    {
        var expectedNodeId = Guid.NewGuid();
        var sut = _tree
            .AddParentNode(expectedNodeId, "root")
            .AddChildNode(nodeId: Guid.NewGuid(), "child of root");

        sut.IsChildOf(expectedNodeId).Should().BeTrue();
    }

    [Fact]
    public void Should_MarkAsSelected()
    {
        var sut = _tree.AddParentNode(nodeId: Guid.NewGuid(), "root");
        
        sut.MarkAsSelected();

        sut.Selected.Should().BeTrue();
    }
}