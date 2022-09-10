using LobsterInk.Adventure.Application.UseCases.OutputPorts;

namespace LobsterInk.Adventure.Application.UseCases.CreateAdventureTree;

public interface ICreateAdventureTreeOutput : IOutputSuccess<Guid>, IOutputValidationError
{
}