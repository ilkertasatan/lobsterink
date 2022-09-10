using LobsterInk.Adventure.Api.UseCases.V1.CreateAdventureTreeNode;
using LobsterInk.Adventure.Application.UseCases.CreateAdventureTreeNode;
using LobsterInk.Adventure.Domain.AdventureTrees;
using LobsterInk.Adventure.Domain.AdventureTrees.Services;
using LobsterInk.Adventure.Infrastructure.Services;
using LobsterInk.Application.Abstraction.Services;
using Moq;

namespace LobsterInk.Adventure.UnitTests.UseCaseTests;

public class CreateAdventureTreeNodeUseCaseTests
{
    [Fact]
    public async Task Should_Execute()
    {
        var treeMock = new Mock<IAdventureTreeRepository>();
        treeMock
            .Setup(x => x.SelectByAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new AdventureTreeFactory().NewTree(treeId: Guid.NewGuid(), "name", userId: Guid.NewGuid()));
        var nodeMock = new Mock<IAdventureTreeNodeRepository>();
        nodeMock
            .Setup(x => x.InsertAsync(It.IsAny<AdventureTreeNode>()))
            .Returns(Task.CompletedTask);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock
            .Setup(x => x.SubmitChangesAsync())
            .Returns(Task.CompletedTask);
        var sut = new CreateAdventureTreeNodeUseCase(unitOfWorkMock.Object, treeMock.Object, nodeMock.Object, new SequentialGuidService());

        await sut.ExecuteAsync(
            new CreateAdventureTreeNodeInput(treeId: Guid.NewGuid(),
                new List<AdventureTreeNodeInput>
                {
                    new("child", new List<AdventureTreeNodeInput>())
                }),
            new CreateAdventureTreeNodePresenter());

        treeMock.Verify(x => x.SelectByAsync(It.IsAny<Guid>()), Times.Once);
        nodeMock.Verify(x => x.InsertAsync(It.IsAny<AdventureTreeNode>()), Times.Once);
        unitOfWorkMock.Verify(x => x.SubmitChangesAsync(), Times.Once);
    }
}