using FluentAssertions;
using LobsterInk.Adventure.Domain.UserAdventures;
using LobsterInk.Adventure.Domain.UserAdventures.Services;
using LobsterInk.Adventure.Infrastructure.DataAccess.Repositories;
using LobsterInk.Application.Abstraction.Services;

namespace LobsterInk.Adventure.IntegrationTests.RepositoryTests;

public class UserAdventureRepositoryTests : IClassFixture<DatabaseFixture>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserAdventureRepository _sut;
    
    public UserAdventureRepositoryTests(DatabaseFixture fixture)
    {
        _unitOfWork = fixture.UnitOfWork;
        _sut = new UserAdventureRepository(_unitOfWork);
    }

    [Fact]
    public async Task Should_InsertAndSelect()
    {
        var expectedTreeId = Guid.NewGuid();
        var expectedUserId = Guid.NewGuid();
        var expectedUserAdventure = new UserAdventureFactory().NewUserAdventure(expectedTreeId, expectedUserId, nodeId: Guid.NewGuid());

        await _sut.InsertAsync(expectedUserAdventure);
        await _unitOfWork.SubmitChangesAsync();

        var actualResult = await _sut.SelectByAsync(expectedTreeId, expectedUserId);
        actualResult.Should().NotBeEmpty().And.Contain(expectedUserAdventure);
    }

    [Fact]
    public async Task Should_Exist()
    {
        var expectedNodeId = Guid.NewGuid();
        var expectedUserAdventure = new UserAdventureFactory().NewUserAdventure(treeId: Guid.NewGuid(), userId: Guid.NewGuid(), expectedNodeId);
        await _sut.InsertAsync(expectedUserAdventure);
        await _unitOfWork.SubmitChangesAsync();
        
        var actualResult = await _sut.ExistsAsync(expectedNodeId);

        actualResult.Should().BeTrue();
    }
}