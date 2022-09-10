using LobsterInk.Application.Abstraction.UseCase;

namespace LobsterInk.Adventure.Application.UseCases.OutputPorts;

public interface IOutputValidationError : IUseCaseOutput
{
    void ValidationError(string message);
}