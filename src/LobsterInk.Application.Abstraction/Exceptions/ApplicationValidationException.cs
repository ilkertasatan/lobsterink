namespace LobsterInk.Application.Abstraction.Exceptions;

public class ApplicationValidationException : Exception
{
    public ApplicationValidationException(IDictionary<string, string[]> errors)
    {
        Errors = errors;
    }

    public IDictionary<string, string[]> Errors { get; }
}