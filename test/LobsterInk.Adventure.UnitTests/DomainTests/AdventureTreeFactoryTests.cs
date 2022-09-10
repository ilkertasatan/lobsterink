using FluentAssertions;
using LobsterInk.Adventure.Domain.AdventureTrees;
using LobsterInk.Adventure.Domain.AdventureTrees.Services;
using LobsterInk.Adventure.Domain.Exceptions;

namespace LobsterInk.Adventure.UnitTests.DomainTests;

public class AdventureTreeFactoryTests
{
    private readonly IAdventureTreeFactory _treeFactory;

    public AdventureTreeFactoryTests()
    {
        _treeFactory = new AdventureTreeFactory();
    }


    [Fact]
    public void Should_CreateNew()
    {
        var sut = _treeFactory.NewTree(treeId: Guid.NewGuid(), "test", userId: Guid.NewGuid());

        sut.Should().BeOfType<AdventureTree>();
    }
    
    [Fact]
    public void Should_ThrowException_Given_InvalidTreeId()
    {
        var act = () =>
        {
            var sut = _treeFactory.NewTree(
                treeId: Guid.Empty,
                name: "test",
                userId: Guid.NewGuid());
        };

        act.Should().Throw<DomainValidationException>();
    }
    
    [Fact]
    public void Should_ThrowException_Given_InvalidUserId()
    {
        var act = () =>
        {
            var sut = _treeFactory.NewTree(
                treeId: Guid.NewGuid(),
                name: "test",
                userId: Guid.Empty);
        };

        act.Should().Throw<DomainValidationException>();
    }
    
    [Fact]
    public void Should_ThrowException_Given_EmptyOrNullName()
    {
        var act = () =>
        {
            var sut = _treeFactory.NewTree(
                treeId: Guid.NewGuid(),
                name: "",
                userId: Guid.NewGuid());
        };

        act.Should().Throw<DomainValidationException>();
    }
}