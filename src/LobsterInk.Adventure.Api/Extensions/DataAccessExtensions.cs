using Microsoft.Data.SqlClient;

namespace LobsterInk.Adventure.Api.Extensions;

public static class DataAccessExtensions
{
    public static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(_ => new SqlConnection(configuration.GetConnectionString("DefaultConnection")));
        return services;
    }
}