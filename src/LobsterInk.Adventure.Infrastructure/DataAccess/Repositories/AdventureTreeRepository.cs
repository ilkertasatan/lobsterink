using Dapper;
using LobsterInk.Adventure.Domain.AdventureTrees;
using LobsterInk.Application.Abstraction.Services;

namespace LobsterInk.Adventure.Infrastructure.DataAccess.Repositories;

public sealed class AdventureTreeRepository : IAdventureTreeRepository
{
    private readonly IUnitOfWork _unitOfWork;

    public AdventureTreeRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task InsertAsync(AdventureTree tree)
    {
        const string sql = @"INSERT INTO AdventureTree (TreeId, Name, UserId, CreatedOn)
                                VALUES (@Id, @Name, @UserId, GETUTCDATE())";

        await _unitOfWork.Connection.ExecuteAsync(sql,
            new
            {
                Id = tree.TreeId,
                tree.Name,
                tree.UserId
            },
            transaction: _unitOfWork.Transaction);
    }
    
    public async Task<AdventureTree> SelectByAsync(Guid treeId)
    {
        const string sql = @"SELECT /**select**/ FROM AdventureTree t /**where**/";
        
        var sqlBuilder = new SqlBuilder();
        var template = sqlBuilder.AddTemplate(sql);

        sqlBuilder
            .Select("t.TreeId")
            .Select("t.Name")
            .Select("t.UserId")
            .Select("t.CreatedOn")
            .Where("t.TreeId = @TreeId", new {TreeId = treeId});
        
        var tree = await _unitOfWork.Connection.QuerySingleAsync<AdventureTree>(
            template.RawSql,
            template.Parameters,
            transaction: _unitOfWork.Transaction);

        return tree ?? AdventureTree.Empty;
    }
}