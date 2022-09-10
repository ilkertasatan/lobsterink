using FluentAssertions;
using LobsterInk.Adventure.Api.UseCases.V1.GetUserAdventure;
using LobsterInk.Adventure.Application.UseCases.GetUserAdventure;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LobsterInk.Adventure.UnitTests.ControllerTests.GetUserAdventure;

public class UserAdventuresControllerTests
{
    private readonly IGetUserAdventureUseCase _useCase;
    private readonly GetUserAdventurePresenter _presenter;

    public UserAdventuresControllerTests()
    {
        var useCaseMock = new Mock<IGetUserAdventureUseCase>();
        useCaseMock
            .Setup(x => x.ExecuteAsync(It.IsAny<GetUserAdventureInput>(), It.IsAny<IGetUserAdventureOutput>()));
        _useCase = useCaseMock.Object;
        
        _presenter = new GetUserAdventurePresenter();
    }
    
    [Fact]
    public async Task Should_Return200()
    {
        _presenter.Success(new List<GetUserAdventureOutput>());
        var sut = new UserAdventuresController(_useCase, _presenter);

        var actualResult = await sut.GetAsync(treeId:Guid.NewGuid(), userId:Guid.NewGuid()) as OkObjectResult;

        actualResult.Should().NotBeNull();
        actualResult!.Value.Should().BeOfType<List<GetUserAdventureResponse>>();
    }

    [Fact]
    public async Task Should_Return404()
    {
        _presenter.ObjectNotFound("");
        var sut = new UserAdventuresController(_useCase, _presenter);

        var actualResult = await sut.GetAsync(treeId:Guid.NewGuid(), userId:Guid.NewGuid()) as NotFoundObjectResult;

        actualResult.Should().NotBeNull();
    }
}