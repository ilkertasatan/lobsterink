using Dapper;
using LobsterInk.Adventure.Domain.AdventureTrees;
using LobsterInk.Application.Abstraction.Services;

namespace LobsterInk.Adventure.Infrastructure.DataAccess.Repositories;

public sealed class AdventureTreeNodeRepository : IAdventureTreeNodeRepository
{
    private readonly IUnitOfWork _unitOfWork;
    
    public AdventureTreeNodeRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task InsertAsync(AdventureTreeNode node)
    {
        const string sql = @"INSERT INTO AdventureTreeNode (NodeId, Name, ParentNodeId, TreeId)
                                VALUES (@NodeId, @Name, @ParentNodeId, @TreeId)";

        await _unitOfWork.Connection.ExecuteAsync(sql,
                param: new
                {
                    node.NodeId,
                    node.Name,
                    node.ParentNodeId,
                    node.TreeId
                },
                transaction: _unitOfWork.Transaction)
            .ConfigureAwait(false);
    }

    public async Task<IEnumerable<AdventureTreeNode>> SelectByAsync(Guid treeId)
    {
        const string sql = @"SELECT /**select**/ FROM AdventureTreeNode n /**where**/";

        var sqlBuilder = new SqlBuilder();
        var template = sqlBuilder.AddTemplate(sql);

        sqlBuilder
            .Select("n.NodeId")
            .Select("n.Name")
            .Select("n.ParentNodeId")
            .Select("n.TreeId")
            .Where("n.TreeId = @TreeId", new {TreeId = treeId});

        var nodes = await _unitOfWork.Connection
            .QueryAsync<AdventureTreeNode>(
                template.RawSql,
                template.Parameters,
                transaction: _unitOfWork.Transaction)
            .ConfigureAwait(false);

        return nodes;
    }
}