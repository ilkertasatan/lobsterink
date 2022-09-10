using DbUp;
using System.Reflection;

namespace LobsterInk.Adventure.Infrastructure.DataAccess.Migrations;

public static class DbMigration
{
    public static void Perform(string connectionString)
    {
        EnsureDatabase.For.SqlDatabase(connectionString);
        
        DeployChanges.To
            .SqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.Load(typeof(DbMigration).Assembly.FullName))
            .LogToConsole()
            .Build()
            .PerformUpgrade();
    }
}