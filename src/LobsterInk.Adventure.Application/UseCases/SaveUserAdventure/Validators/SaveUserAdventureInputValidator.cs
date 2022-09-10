using FluentValidation;

namespace LobsterInk.Adventure.Application.UseCases.SaveUserAdventure.Validators;

public sealed class SaveUserAdventureInputValidator : AbstractValidator<SaveUserAdventureInput>
{
    public SaveUserAdventureInputValidator()
    {
        RuleFor(i => i.TreeId).NotEmpty().WithMessage("TreeId cannot be empty");
        RuleFor(i => i.UserId).NotEmpty().WithMessage("UserId cannot be empty");
        RuleFor(i => i.NodeId).NotEmpty().WithMessage("NodeId cannot be empty");
    }
}