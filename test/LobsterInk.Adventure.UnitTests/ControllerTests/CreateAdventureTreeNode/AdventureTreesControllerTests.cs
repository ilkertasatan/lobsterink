using FluentAssertions;
using LobsterInk.Adventure.Api.UseCases.V1.CreateAdventureTreeNode;
using LobsterInk.Adventure.Application.UseCases.CreateAdventureTreeNode;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LobsterInk.Adventure.UnitTests.ControllerTests.CreateAdventureTreeNode;

public class AdventureTreesControllerTests
{
    private readonly ICreateAdventureTreeNodeUseCase _useCase;
    private readonly CreateAdventureTreeNodePresenter _presenter;

    public AdventureTreesControllerTests()
    {
        var useCaseMock = new Mock<ICreateAdventureTreeNodeUseCase>();
        useCaseMock
            .Setup(x => x.ExecuteAsync(It.IsAny<CreateAdventureTreeNodeInput>(), It.IsAny<ICreateAdventureTreeNodeOutput>()));
        _useCase = useCaseMock.Object;
        
        _presenter = new CreateAdventureTreeNodePresenter();
    }
    
    [Fact]
    public async Task Should_Return200()
    {
        _presenter.Success();
        var sut = new AdventureTreesController(_useCase, _presenter);

        var actualResult = await sut.CreateAsync(treeId: Guid.NewGuid(), new List<CreateAdventureTreeNodeRequest>()) as CreatedResult;

        actualResult.Should().NotBeNull();
    }

    [Fact]
    public async Task Should_Return400()
    {
        _presenter.ValidationError("");
        var sut = new AdventureTreesController(_useCase, _presenter);

        var actualResult = await sut.CreateAsync(treeId: Guid.NewGuid(), new List<CreateAdventureTreeNodeRequest>()) as BadRequestObjectResult;

        actualResult.Should().NotBeNull();
    }
    
    [Fact]
    public async Task Should_Return404()
    {
        _presenter.ObjectNotFound("");
        var sut = new AdventureTreesController(_useCase, _presenter);

        var actualResult = await sut.CreateAsync(treeId: Guid.NewGuid(), new List<CreateAdventureTreeNodeRequest>()) as NotFoundObjectResult;

        actualResult.Should().NotBeNull();
    }
}