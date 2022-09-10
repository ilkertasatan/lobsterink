using FluentAssertions;
using LobsterInk.Adventure.Api.UseCases.V1.GetAdventureTree;
using LobsterInk.Adventure.Application.UseCases.GetAdventureTree;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LobsterInk.Adventure.UnitTests.ControllerTests.GetAdventureTree;

public class AdventureTreesControllerTests
{
    private readonly IGetAdventureTreeUseCase _useCase;
    private readonly GetAdventureTreePresenter _presenter;

    public AdventureTreesControllerTests()
    {
        var useCaseMock = new Mock<IGetAdventureTreeUseCase>();
        useCaseMock
            .Setup(x => x.ExecuteAsync(It.IsAny<GetAdventureTreeInput>(), It.IsAny<IGetAdventureTreeOutput>()));
        _useCase = useCaseMock.Object;
        
        _presenter = new GetAdventureTreePresenter();
    }
    
    [Fact]
    public async Task Should_Return200()
    {
        _presenter.Success(new List<GetAdventureTreeOutput>());
        var sut = new AdventureTreesController(_useCase, _presenter);

        var actualResult = await sut.GetAsync(treeId: Guid.NewGuid()) as OkObjectResult;

        actualResult.Should().NotBeNull();
        actualResult!.Value.Should().BeOfType<List<GetAdventureTreeResponse>>();
    }

    [Fact]
    public async Task Should_Return404()
    {
        _presenter.ObjectNotFound("");
        var sut = new AdventureTreesController(_useCase, _presenter);

        var actualResult = await sut.GetAsync(treeId: Guid.NewGuid()) as NotFoundObjectResult;

        actualResult.Should().NotBeNull();
    }
}