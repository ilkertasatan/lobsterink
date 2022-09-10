using FluentAssertions;
using LobsterInk.Adventure.Domain.AdventureTrees;
using LobsterInk.Adventure.Domain.AdventureTrees.Services;
using LobsterInk.Adventure.Domain.UserAdventures;
using LobsterInk.Adventure.Domain.UserAdventures.Services;

namespace LobsterInk.Adventure.UnitTests.DomainTests;

public class AdventureTreeUnitTests
{
    private readonly IAdventureTreeFactory _treeFactory;
    private readonly IUserAdventureFactory _userAdventureFactory;

    public AdventureTreeUnitTests()
    {
        _treeFactory = new AdventureTreeFactory();
        _userAdventureFactory = new UserAdventureFactory();
    }

    [Fact]
    public void Should_Exist()
    {
        var sut = _treeFactory.NewTree(treeId: Guid.NewGuid(), "test", userId: Guid.NewGuid());

        var actualResult = sut.Exists();

        actualResult.Should().BeTrue();
    }

    [Fact]
    public void Should_BuildTree()
    {
        var sut = _treeFactory.NewTree(treeId: Guid.NewGuid(), "test", userId: Guid.NewGuid());
        var parentNode = sut.AddParentNode(nodeId: Guid.NewGuid(), "root");
        var childNode = parentNode.AddChildNode(nodeId: Guid.NewGuid(), "child of root");

        var actualResult = sut.Build(new List<AdventureTreeNode>
        {
            parentNode,
            childNode
        });

        actualResult.Nodes.Should().NotBeEmpty().And.Contain(parentNode);
        parentNode.Nodes.Should().NotBeEmpty().And.Contain(childNode);
    }

    [Fact]
    public void Should_AddUserChoices()
    {
        var sut = _treeFactory.NewTree(treeId: Guid.NewGuid(), "test", userId: Guid.NewGuid());
        var userAdventure = _userAdventureFactory.NewUserAdventure(sut.TreeId, sut.UserId, nodeId: Guid.NewGuid());

        var actualResult = sut.AddUserChoices(new List<UserAdventure>
        {
            userAdventure
        });

        actualResult.UserChoices.Should().NotBeEmpty().And.Contain(userAdventure);
    }

    [Fact]
    public void Should_AddParentNode()
    {
        var sut = _treeFactory.NewTree(treeId: Guid.NewGuid(), "test", userId: Guid.NewGuid());

        var actualResult = sut.AddParentNode(nodeId: Guid.NewGuid(), "root");

        sut.Nodes.Should().NotBeEmpty().And.Contain(actualResult);
    }

    [Fact]
    public void Should_MarkTheNodeAsSelected()
    {
        var sut = _treeFactory.NewTree(treeId: Guid.NewGuid(), "test", userId: Guid.NewGuid());
        var nodeId = Guid.NewGuid();
        sut.AddParentNode(nodeId, "root");
        var userAdventure = _userAdventureFactory.NewUserAdventure(sut.TreeId, sut.UserId, nodeId);
        sut.AddUserChoices(new List<UserAdventure> {userAdventure});

        sut.MarkAsSelected();

        userAdventure.IsSelected(nodeId).Should().BeTrue();
    }
}