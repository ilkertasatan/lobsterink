using LobsterInk.Adventure.Application.UseCases.OutputPorts;

namespace LobsterInk.Adventure.Application.UseCases.CreateAdventureTreeNode;

public interface ICreateAdventureTreeNodeOutput : IOutputSuccess, IOutputValidationError, IOutputObjectNotFound
{
}