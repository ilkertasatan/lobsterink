using FluentAssertions;
using LobsterInk.Adventure.Api.UseCases.V1.CreateAdventureTree;
using LobsterInk.Adventure.Application.UseCases.CreateAdventureTree;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LobsterInk.Adventure.UnitTests.ControllerTests.CreateAdventureTree;

public class AdventureTreesControllerTests
{
    private readonly ICreateAdventureTreeUseCase _useCase;
    private readonly CreateAdventureTreePresenter _presenter;

    public AdventureTreesControllerTests()
    {
        var useCaseMock = new Mock<ICreateAdventureTreeUseCase>();
        useCaseMock
            .Setup(x => x.ExecuteAsync(It.IsAny<CreateAdventureTreeInput>(), It.IsAny<ICreateAdventureTreeOutput>()));
        _useCase = useCaseMock.Object;
        
        _presenter = new CreateAdventureTreePresenter();
    }
    
    [Fact]
    public async Task Should_Return200()
    {
        _presenter.Success(Guid.NewGuid());
        var sut = new AdventureTreesController(_useCase, _presenter);

        var actualResult = await sut.CreateAsync(new CreateAdventureTreeRequest()) as CreatedResult;

        actualResult.Should().NotBeNull();
    }

    [Fact]
    public async Task Should_Return400()
    {
        _presenter.ValidationError("");
        var sut = new AdventureTreesController(_useCase, _presenter);

        var actualResult = await sut.CreateAsync(new CreateAdventureTreeRequest()) as BadRequestObjectResult;

        actualResult.Should().NotBeNull();
    }
}