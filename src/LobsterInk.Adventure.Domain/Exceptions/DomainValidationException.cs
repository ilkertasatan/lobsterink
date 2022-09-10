namespace LobsterInk.Adventure.Domain.Exceptions;

public class DomainValidationException : DomainException
{
    public DomainValidationException(string message) : base(message)
    {
    }
}