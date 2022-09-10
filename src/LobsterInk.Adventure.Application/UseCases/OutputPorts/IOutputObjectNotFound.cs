namespace LobsterInk.Adventure.Application.UseCases.OutputPorts;

public interface IOutputObjectNotFound
{
    void ObjectNotFound(string message);
}