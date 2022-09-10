using FluentAssertions;
using LobsterInk.Adventure.Domain.AdventureTrees.Services;
using LobsterInk.Adventure.Infrastructure.DataAccess.Repositories;
using LobsterInk.Application.Abstraction.Services;

namespace LobsterInk.Adventure.IntegrationTests.RepositoryTests;

public class AdventureTreeRepositoryTests : IClassFixture<DatabaseFixture>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public AdventureTreeRepositoryTests(DatabaseFixture fixture)
    {
        _unitOfWork = fixture.UnitOfWork;
    }

    [Fact]
    public async Task Should_InsertAndSelect()
    {
        var expectedTreeId = Guid.NewGuid();
        var sut = new AdventureTreeRepository(_unitOfWork);
        var expectedTree = new AdventureTreeFactory().NewTree(expectedTreeId, name: "test", userId: Guid.NewGuid());

        await sut.InsertAsync(expectedTree);
        await _unitOfWork.SubmitChangesAsync();
        
        var actualResult = await sut.SelectByAsync(expectedTreeId);
        actualResult.Should().NotBeNull().And.BeEquivalentTo(expectedTree);
    }
}