namespace LobsterInk.Application.Abstraction.UseCase;

public interface IUseCase<in TInput, in TOutput>
    where TInput : IUseCaseInput
    where TOutput : IUseCaseOutput
{
    Task ExecuteAsync(TInput input, TOutput output);
}