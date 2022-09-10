using Microsoft.Data.SqlClient;

namespace LobsterInk.Application.Abstraction.Services;

public interface IUnitOfWork : IDisposable
{
    public SqlConnection Connection { get; }
    
    public SqlTransaction Transaction { get; }

    Task SubmitChangesAsync();
}