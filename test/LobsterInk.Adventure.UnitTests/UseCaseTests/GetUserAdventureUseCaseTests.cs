using LobsterInk.Adventure.Api.UseCases.V1.GetUserAdventure;
using LobsterInk.Adventure.Application.UseCases.GetUserAdventure;
using LobsterInk.Adventure.Domain.AdventureTrees;
using LobsterInk.Adventure.Domain.AdventureTrees.Services;
using LobsterInk.Adventure.Domain.UserAdventures;
using LobsterInk.Adventure.Domain.UserAdventures.Services;
using Moq;

namespace LobsterInk.Adventure.UnitTests.UseCaseTests;

public class GetUserAdventureUseCaseTests
{
    [Fact]
    public async Task Should_Execute()
    {
        var expectedTree = new AdventureTreeFactory().NewTree(treeId: Guid.NewGuid(), "name", userId: Guid.NewGuid());
        var expectedNode = expectedTree.AddParentNode(nodeId: Guid.NewGuid(), "name");
        var expectedUserAdventure = new UserAdventureFactory().NewUserAdventure(expectedTree.TreeId, expectedTree.UserId, expectedNode.NodeId);
        var treeMock = new Mock<IAdventureTreeRepository>();
        treeMock
            .Setup(x => x.SelectByAsync(It.IsAny<Guid>()))
            .ReturnsAsync(expectedTree);
        var nodeMock = new Mock<IAdventureTreeNodeRepository>();
        nodeMock
            .Setup(x => x.SelectByAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new[] {expectedNode});
        var userAdventureMock = new Mock<IUserAdventureRepository>();
        userAdventureMock
            .Setup(x => x.SelectByAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(new[] {expectedUserAdventure});
        var sut = new GetUserAdventureUseCase(treeMock.Object, nodeMock.Object, userAdventureMock.Object);

        await sut.ExecuteAsync(
            new GetUserAdventureInput(treeId: Guid.NewGuid(), userId: Guid.NewGuid()),
            new GetUserAdventurePresenter());

        treeMock.Verify(x => x.SelectByAsync(It.IsAny<Guid>()), Times.Once);
        nodeMock.Verify(x => x.SelectByAsync(It.IsAny<Guid>()), Times.Once);
        userAdventureMock.Verify(x => x.SelectByAsync(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
    } 
}