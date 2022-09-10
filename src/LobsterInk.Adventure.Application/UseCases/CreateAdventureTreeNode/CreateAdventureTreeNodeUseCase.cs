using LobsterInk.Adventure.Application.UseCases.CreateAdventureTreeNode.Validators;
using LobsterInk.Adventure.Domain.AdventureTrees;
using LobsterInk.Adventure.Domain.Exceptions;
using LobsterInk.Application.Abstraction.Exceptions;
using LobsterInk.Application.Abstraction.Services;

namespace LobsterInk.Adventure.Application.UseCases.CreateAdventureTreeNode;

public sealed class CreateAdventureTreeNodeUseCase : ICreateAdventureTreeNodeUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAdventureTreeRepository _trees;
    private readonly IAdventureTreeNodeRepository _nodes;
    private readonly ISequentialGuid _guid;

    public CreateAdventureTreeNodeUseCase(
        IUnitOfWork unitOfWork,
        IAdventureTreeRepository trees,
        IAdventureTreeNodeRepository nodes,
        ISequentialGuid guid)
    {
        _unitOfWork = unitOfWork;
        _trees = trees;
        _nodes = nodes;
        _guid = guid;
    }

    public async Task ExecuteAsync(CreateAdventureTreeNodeInput input, ICreateAdventureTreeNodeOutput output)
    {
        var result = await new CreateAdventureTreeNodeInputValidator().ValidateAsync(input);
        if (!result.IsValid) throw new ApplicationValidationException(result.ToDictionary());
        
        try
        {
            var tree = await _trees.SelectByAsync(input.TreeId);
            if (!tree.Exists())
            {
                output.ObjectNotFound($"AdventureTree with Id:'{input.TreeId}' does not exist");
                return;
            }

            foreach (var node in input.Nodes)
            {
                var parentNode = tree.AddParentNode(_guid.NewGuid(), node.Name);
                await _nodes.InsertAsync(parentNode);

                await NewNodesOf(parentNode, node.Nodes.ToList());
            }
            
            await _unitOfWork.SubmitChangesAsync();
            
            output.Success();
        }
        catch (DomainValidationException exception)
        {
            output.ValidationError(exception.Message);
        }
    }

    private async Task NewNodesOf(AdventureTreeNode parentNode, List<AdventureTreeNodeInput> nodes)
    {
        if (!nodes.Any()) return;

        foreach (var node in nodes)
        {
            var childNode = parentNode.AddChildNode(_guid.NewGuid(), node.Name);
            await _nodes.InsertAsync(childNode);
            
            await NewNodesOf(childNode, node.Nodes.ToList());
        }
    }
}