using Microsoft.EntityFrameworkCore;
using ProductsApi.Modules.Shared.Db;

namespace Migrations;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var productsDbConnectionString = CreateConnection("PRODUCTS_DB_CONNECTION_STRING");
        await using var productsDb = new ProductsDbContext(new DbContextOptionsBuilder<ProductsDbContext>()
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