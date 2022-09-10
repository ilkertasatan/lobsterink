using LobsterInk.Adventure.Api.UseCases.V1.SaveUserAdventure;
using LobsterInk.Adventure.Application.UseCases.SaveUserAdventure;
using LobsterInk.Adventure.Domain.AdventureTrees;
using LobsterInk.Adventure.Domain.AdventureTrees.Services;
using LobsterInk.Adventure.Domain.UserAdventures;
using LobsterInk.Adventure.Domain.UserAdventures.Services;
using LobsterInk.Application.Abstraction.Services;
using Moq;

namespace LobsterInk.Adventure.UnitTests.UseCaseTests;

public class SaveUserAdventureUseCaseTest
{
    [Fact]
    public async Task Should_Execute()
    {
        var expectedTree = new AdventureTreeFactory().NewTree(treeId: Guid.NewGuid(), "name", userId: Guid.NewGuid());
        var treeMock = new Mock<IAdventureTreeRepository>();
        treeMock
            .Setup(x => x.SelectByAsync(It.IsAny<Guid>()))
            .ReturnsAsync(expectedTree);
        var userAdventureMock = new Mock<IUserAdventureRepository>();
        userAdventureMock
            .Setup(x => x.InsertAsync(It.IsAny<UserAdventure>()));
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock
            .Setup(x => x.SubmitChangesAsync())
            .Returns(Task.CompletedTask);
        var sut = new SaveUserAdventureUseCase(new UserAdventureFactory(),unitOfWorkMock.Object, treeMock.Object, userAdventureMock.Object);

        await sut.ExecuteAsync(
            new SaveUserAdventureInput(expectedTree.TreeId, expectedTree.UserId, nodeId: Guid.NewGuid()),
            new SaveUserAdventurePresenter());

        treeMock.Verify(x => x.SelectByAsync(It.IsAny<Guid>()), Times.Once);
        userAdventureMock.Verify(x => x.InsertAsync(It.IsAny<UserAdventure>()), Times.Once);
        unitOfWorkMock.Verify(x => x.SubmitChangesAsync(), Times.Once);
    }  
}