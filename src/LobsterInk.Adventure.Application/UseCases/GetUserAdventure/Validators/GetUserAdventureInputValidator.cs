using FluentValidation;

namespace LobsterInk.Adventure.Application.UseCases.GetUserAdventure.Validators;

public sealed class GetUserAdventureInputValidator : AbstractValidator<GetUserAdventureInput>
{
    public GetUserAdventureInputValidator()
    {
        RuleFor(i => i.TreeId).NotEmpty().WithMessage("TreeId cannot be empty");
        RuleFor(i => i.UserId).NotEmpty().WithMessage("UserId cannot be empty");
    }
}