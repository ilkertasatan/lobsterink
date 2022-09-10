using LobsterInk.Adventure.Infrastructure.DataAccess;
using LobsterInk.Adventure.Infrastructure.DataAccess.Migrations;
using LobsterInk.Application.Abstraction.Services;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace LobsterInk.Adventure.IntegrationTests;

public class DatabaseFixture
{
    private readonly string _connectionString;

    public DatabaseFixture()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, false)
            .Build();

        _connectionString = configuration.GetConnectionString("DefaultConnection");
        DbMigration.Perform(_connectionString);
    }

    public IUnitOfWork UnitOfWork => new UnitOfWork(new SqlConnection(_connectionString));
}