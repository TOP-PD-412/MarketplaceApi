using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductsApi.Core.Constants;
using ProductsApi.Modules.Shared.Db;

namespace Migrations;

public class Program
{
    public static async Task Main(string[] args)
    {
        var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<Program>();

        logger.LogInformation("Migrations started...");
        try
        {
            var productsDbConnectionString = CreateConnection(Config.Envs.Db.Connection);
            await using var productsDb = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql(productsDbConnectionString).Options);
            await productsDb.Database.MigrateAsync();
            logger.LogInformation("Migrations succeeded.");
        }
        catch (InvalidOperationException)
        {
            logger.LogError($"Connection string {Config.Envs.Db.Connection} is missing.");
        }
    }

    private static string CreateConnection(string connectionStringEnv)
    {
        var dbConnectionString = Environment.GetEnvironmentVariable(connectionStringEnv);
        return string.IsNullOrWhiteSpace(dbConnectionString)
            ? throw new InvalidOperationException("Connection string is empty.")
            : dbConnectionString;
    }
}