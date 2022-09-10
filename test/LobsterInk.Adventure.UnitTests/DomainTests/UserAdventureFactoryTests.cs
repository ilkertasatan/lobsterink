using FluentAssertions;
using LobsterInk.Adventure.Domain.Exceptions;
using LobsterInk.Adventure.Domain.UserAdventures;
using LobsterInk.Adventure.Domain.UserAdventures.Services;

namespace LobsterInk.Adventure.UnitTests.DomainTests;

public class UserAdventureFactoryTests
{
    private readonly IUserAdventureFactory _userAdventureFactory;

    public UserAdventureFactoryTests()
    {
        _userAdventureFactory = new UserAdventureFactory();
    }

    [Fact]
    public void Should_CreateNew()
    {
        var sut = _userAdventureFactory.NewUserAdventure(
            treeId: Guid.NewGuid(),
            userId: Guid.NewGuid(),
            nodeId: Guid.NewGuid());

        sut.Should().BeOfType<UserAdventure>();
    }

    [Fact]
    public void Should_ThrowException_Given_InvalidTreeId()
    {
        var act = () =>
        {
            var sut = _userAdventureFactory.NewUserAdventure(
                treeId: Guid.Empty,
                userId: Guid.NewGuid(),
                nodeId: Guid.NewGuid());
        };

        act.Should().Throw<DomainValidationException>();
    }
    
    [Fact]
    public void Should_ThrowException_Given_InvalidUserId()
    {
        var act = () =>
        {
            var sut = _userAdventureFactory.NewUserAdventure(
                treeId: Guid.NewGuid(),
                userId: Guid.Empty,
                nodeId: Guid.NewGuid());
        };

        act.Should().Throw<DomainValidationException>();
    }
    
    [Fact]
    public void Should_ThrowException_Given_InvalidNodeId()
    {
        var act = () =>
        {
            var sut = _userAdventureFactory.NewUserAdventure(
                treeId: Guid.NewGuid(),
                userId: Guid.NewGuid(),
                nodeId: Guid.Empty);
        };

        act.Should().Throw<DomainValidationException>();
    }
}