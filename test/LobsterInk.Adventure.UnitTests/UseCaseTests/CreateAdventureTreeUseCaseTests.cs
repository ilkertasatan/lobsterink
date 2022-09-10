using LobsterInk.Adventure.Api.UseCases.V1.CreateAdventureTree;
using LobsterInk.Adventure.Application.UseCases.CreateAdventureTree;
using LobsterInk.Adventure.Domain.AdventureTrees;
using LobsterInk.Adventure.Domain.AdventureTrees.Services;
using LobsterInk.Adventure.Infrastructure.Services;
using LobsterInk.Application.Abstraction.Services;
using Moq;

namespace LobsterInk.Adventure.UnitTests.UseCaseTests;

public class CreateAdventureTreeUseCaseTests
{
    [Fact]
    public async Task Should_Execute()
    {
        var repositoryMock = new Mock<IAdventureTreeRepository>();
        repositoryMock
            .Setup(x => x.InsertAsync(It.IsAny<AdventureTree>()))
            .Returns(Task.CompletedTask);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock
            .Setup(x => x.SubmitChangesAsync())
            .Returns(Task.CompletedTask);
        var sut = new CreateAdventureTreeUseCase(new AdventureTreeFactory(), unitOfWorkMock.Object, repositoryMock.Object, new SequentialGuidService());

        await sut.ExecuteAsync(
            new CreateAdventureTreeInput("name", userId: Guid.NewGuid()),
            new CreateAdventureTreePresenter());

        repositoryMock.Verify(x => x.InsertAsync(It.IsAny<AdventureTree>()), Times.Once);
        unitOfWorkMock.Verify(x => x.SubmitChangesAsync(), Times.Once);
    }
}