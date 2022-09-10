using FluentAssertions;
using LobsterInk.Adventure.Domain.AdventureTrees.Services;
using LobsterInk.Adventure.Infrastructure.DataAccess.Repositories;
using LobsterInk.Application.Abstraction.Services;

namespace LobsterInk.Adventure.IntegrationTests.RepositoryTests;

public class AdventureTreeNodeRepositoryTests : IClassFixture<DatabaseFixture>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public AdventureTreeNodeRepositoryTests(DatabaseFixture fixture)
    {
        _unitOfWork = fixture.UnitOfWork;
    }
    
    [Fact]
    public async Task Should_InsertAndSelect()
    {
        var expectedTreeId = Guid.NewGuid();
        var expectedTree = new AdventureTreeFactory().NewTree(expectedTreeId, name: "name", userId: Guid.NewGuid());
        await new AdventureTreeRepository(_unitOfWork).InsertAsync(expectedTree);
        var sut = new AdventureTreeNodeRepository(_unitOfWork);
        var expectedNode = expectedTree.AddParentNode(nodeId: Guid.NewGuid(), "root");
        
        await sut.InsertAsync(expectedNode);
        await _unitOfWork.SubmitChangesAsync();
        
        var actualResult = await sut.SelectByAsync(expectedTreeId);
        actualResult.Should().NotBeEmpty().And.Contain(expectedNode);
    }
}