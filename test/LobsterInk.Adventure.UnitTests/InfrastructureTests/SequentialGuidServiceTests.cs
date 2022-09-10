using FluentAssertions;
using LobsterInk.Adventure.Infrastructure.Services;

namespace LobsterInk.Adventure.UnitTests.InfrastructureTests;

public class SequentialGuidServiceTests
{
    [Fact]
    public void Should_CreateNew()
    {
        var sut = new SequentialGuidService();

        var actualResult = sut.NewGuid();

        actualResult.Should().NotBeEmpty();
    }
}