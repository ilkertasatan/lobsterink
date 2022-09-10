using FluentValidation;

namespace LobsterInk.Adventure.Application.UseCases.GetAdventureTree.Validators;

public sealed class GetAdventureTreeInputValidator : AbstractValidator<GetAdventureTreeInput>
{
    public GetAdventureTreeInputValidator()
    {
        RuleFor(i => i.TreeId).NotEmpty().WithMessage("TreeId cannot be empty");
    }
}