using Dapper;
using LobsterInk.Adventure.Domain.UserAdventures;
using LobsterInk.Application.Abstraction.Services;

namespace LobsterInk.Adventure.Infrastructure.DataAccess.Repositories;

public sealed class UserAdventureRepository : IUserAdventureRepository
{
    private readonly IUnitOfWork _unitOfWork;

    public UserAdventureRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task InsertAsync(UserAdventure userAdventure)
    {
        const string sql = @"INSERT INTO UserAdventure (TreeId, UserId, NodeId)
                                VALUES (@TreeId, @UserId, @NodeId)";

        await _unitOfWork.Connection.ExecuteAsync(sql,
            new
            {
                userAdventure.TreeId,
                userAdventure.UserId,
                userAdventure.NodeId
            },
            transaction: _unitOfWork.Transaction);
    }

    public async Task<bool> ExistsAsync(Guid nodeId)
    {
        const string sql = "SELECT COUNT(1) from UserAdventure WHERE NodeId = @NodeId";
        return await _unitOfWork.Connection.ExecuteScalarAsync<bool>(sql, new {nodeId}, _unitOfWork.Transaction);
    }

    public async Task<IEnumerable<UserAdventure>> SelectByAsync(Guid treeId, Guid userId)
    {
        const string sql = @"SELECT /**select**/ FROM UserAdventure u /**where**/";

        var sqlBuilder = new SqlBuilder();
        var template = sqlBuilder.AddTemplate(sql);

        sqlBuilder
            .Select("u.TreeId")
            .Select("u.UserId")
            .Select("u.NodeId")
            .Where("u.TreeId = @TreeId AND u.UserId = @UserId", new {TreeId = treeId, UserId = userId});

        var userAdventures = await _unitOfWork.Connection.QueryAsync<UserAdventure>(
            template.RawSql,
            template.Parameters,
            transaction: _unitOfWork.Transaction);

        return userAdventures;
    }
}