using Microsoft.EntityFrameworkCore;
using ProductsApi.Core.Constants;
using ProductsApi.Modules.Shared.Db;

namespace Migrations;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var productsDbConnectionString = CreateConnection(Config.Envs.Db.Connection);
        await using var productsDb = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(productsDbConnectionString).Options);
        await productsDb.Database.MigrateAsync();
    }

    private static string CreateConnection(string connectionStringEnv)
    {
        var dbConnectionString = Environment.GetEnvironmentVariable(connectionStringEnv);
        return string.IsNullOrWhiteSpace(dbConnectionString)
            ? throw new InvalidOperationException("Connection string is empty.")
            : dbConnectionString;
    }
}