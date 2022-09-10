using LobsterInk.Application.Abstraction.Services;
using Microsoft.Data.SqlClient;

namespace LobsterInk.Adventure.Infrastructure.DataAccess;

public sealed class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(SqlConnection sqlConnection)
    {
        Connection = sqlConnection;
        Connection.Open();
        Transaction = sqlConnection.BeginTransaction();
    }
    
    public SqlConnection Connection { get; }
    
    public SqlTransaction Transaction { get; }

    public async Task SubmitChangesAsync()
    {
        try
        {
            await Transaction.CommitAsync();
        }
        catch (Exception)
        {
            await Transaction.RollbackAsync();
        }
    }

    public void Dispose()
    {
        Transaction.Connection?.Close();
        Transaction.Connection?.Dispose();
        Connection.Dispose();
    }
}