using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Migrations;

public class Program
{
    public static async Task Main(string[] args)
    {
       var logger = LoggerFactory.Create(builder => builder.AddConsole())
            .CreateLogger<Program>();

        logger.LogInformation("Migrations started...");
        var dbCtxFactory = new AppDbContextFactory();
        await using var ctx = dbCtxFactory.CreateDbContext(args);
        await ctx.Database.MigrateAsync();
        logger.LogInformation("Migrations succeeded.");
    }
}