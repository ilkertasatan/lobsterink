using LobsterInk.Adventure.Application.UseCases.OutputPorts;

namespace LobsterInk.Adventure.Application.UseCases.GetUserAdventure;

public interface IGetUserAdventureOutput : IOutputSuccess<IEnumerable<GetUserAdventureOutput>>, IOutputObjectNotFound
{
}