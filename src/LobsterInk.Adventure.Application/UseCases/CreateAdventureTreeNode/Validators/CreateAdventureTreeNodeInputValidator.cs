using FluentValidation;

namespace LobsterInk.Adventure.Application.UseCases.CreateAdventureTreeNode.Validators;

public sealed class CreateAdventureTreeNodeInputValidator : AbstractValidator<CreateAdventureTreeNodeInput>
{
    public CreateAdventureTreeNodeInputValidator()
    {
        RuleFor(i => i.TreeId).NotEmpty().WithMessage("TreeId cannot be empty");
    }
}