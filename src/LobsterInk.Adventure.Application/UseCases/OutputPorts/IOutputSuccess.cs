using LobsterInk.Application.Abstraction.UseCase;

namespace LobsterInk.Adventure.Application.UseCases.OutputPorts;

public interface IOutputSuccess : IUseCaseOutput
{
    void Success();
}

public interface IOutputSuccess<in TOutput> : IUseCaseOutput
{
    void Success(TOutput output);
}