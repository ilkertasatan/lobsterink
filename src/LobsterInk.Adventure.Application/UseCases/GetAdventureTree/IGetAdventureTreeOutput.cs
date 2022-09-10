using LobsterInk.Adventure.Application.UseCases.OutputPorts;

namespace LobsterInk.Adventure.Application.UseCases.GetAdventureTree;

public interface IGetAdventureTreeOutput : IOutputSuccess<IEnumerable<GetAdventureTreeOutput>>, IOutputObjectNotFound
{
}