using FluentValidation;

namespace LobsterInk.Adventure.Application.UseCases.CreateAdventureTree.Validators;

public sealed class CreateAdventureTreeInputValidator : AbstractValidator<CreateAdventureTreeInput>
{
    public CreateAdventureTreeInputValidator()
    {
        RuleFor(i => i.Name).NotEmpty().WithMessage("{PropertyName} cannot be empty");
        RuleFor(i => i.UserId).NotEmpty().WithMessage("{PropertyName} cannot be empty");
    }
}