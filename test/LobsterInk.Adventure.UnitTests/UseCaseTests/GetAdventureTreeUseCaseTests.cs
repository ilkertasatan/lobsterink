using LobsterInk.Adventure.Api.UseCases.V1.GetAdventureTree;
using LobsterInk.Adventure.Application.UseCases.GetAdventureTree;
using LobsterInk.Adventure.Domain.AdventureTrees;
using LobsterInk.Adventure.Domain.AdventureTrees.Services;
using Moq;

namespace LobsterInk.Adventure.UnitTests.UseCaseTests;

public class GetAdventureTreeUseCaseTests
{
    [Fact]
    public async Task Should_Execute()
    {
        var expectedTree = new AdventureTreeFactory().NewTree(treeId: Guid.NewGuid(), "name", userId: Guid.NewGuid());
        var treeMock = new Mock<IAdventureTreeRepository>();
        treeMock
            .Setup(x => x.SelectByAsync(It.IsAny<Guid>()))
            .ReturnsAsync(expectedTree);
        var nodeMock = new Mock<IAdventureTreeNodeRepository>();
        nodeMock
            .Setup(x => x.SelectByAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new[] {expectedTree.AddParentNode(nodeId: Guid.NewGuid(), "name")});
        var sut = new GetAdventureTreeUseCase(treeMock.Object, nodeMock.Object);

        await sut.ExecuteAsync(
            new GetAdventureTreeInput(treeId:Guid.NewGuid()),
            new GetAdventureTreePresenter());

        treeMock.Verify(x => x.SelectByAsync(It.IsAny<Guid>()), Times.Once);
        nodeMock.Verify(x => x.SelectByAsync(It.IsAny<Guid>()), Times.Once);
    }
}