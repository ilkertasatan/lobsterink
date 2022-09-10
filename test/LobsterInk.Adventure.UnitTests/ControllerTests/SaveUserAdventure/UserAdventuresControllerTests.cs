using FluentAssertions;
using LobsterInk.Adventure.Api.UseCases.V1.SaveUserAdventure;
using LobsterInk.Adventure.Application.UseCases.SaveUserAdventure;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LobsterInk.Adventure.UnitTests.ControllerTests.SaveUserAdventure;

public class UserAdventuresControllerTests
{
    private readonly ISaveUserAdventureUseCase _useCase;
    private readonly SaveUserAdventurePresenter _presenter;

    public UserAdventuresControllerTests()
    {
        var useCaseMock = new Mock<ISaveUserAdventureUseCase>();
        useCaseMock
            .Setup(x => x.ExecuteAsync(It.IsAny<SaveUserAdventureInput>(), It.IsAny<ISaveUserAdventureOutput>()));
        _useCase = useCaseMock.Object;
        
        _presenter = new SaveUserAdventurePresenter();
    }
    
    [Fact]
    public async Task Should_Return201()
    {
        _presenter.Success();
        var sut = new UserAdventuresController(_useCase, _presenter);

        var actualResult = await sut.SaveAsync(new SaveUserAdventureRequest()) as CreatedResult;

        actualResult.Should().NotBeNull();
    }

    [Fact]
    public async Task Should_Return404()
    {
        _presenter.ObjectNotFound("");
        var sut = new UserAdventuresController(_useCase, _presenter);

        var actualResult = await sut.SaveAsync(new SaveUserAdventureRequest()) as NotFoundObjectResult;

        actualResult.Should().NotBeNull();
    }
    
    [Fact]
    public async Task Should_Return400()
    {
        _presenter.ValidationError("");
        var sut = new UserAdventuresController(_useCase, _presenter);

        var actualResult = await sut.SaveAsync(new SaveUserAdventureRequest()) as BadRequestObjectResult;

        actualResult.Should().NotBeNull();
    }
}