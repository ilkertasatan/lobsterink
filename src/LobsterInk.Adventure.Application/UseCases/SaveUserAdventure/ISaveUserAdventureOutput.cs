using LobsterInk.Adventure.Application.UseCases.OutputPorts;

namespace LobsterInk.Adventure.Application.UseCases.SaveUserAdventure;

public interface ISaveUserAdventureOutput : IOutputSuccess, IOutputValidationError, IOutputObjectNotFound
{
}