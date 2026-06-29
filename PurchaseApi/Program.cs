using dotenv.net;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using PurchaseApi.Purchase;
using Shared.Constants;
using Shared.Infrastructure;
using Shared.Products;
using Shared.Purchases;
using Shared.Users;
using Shared.Utils;

namespace PurchaseApi;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        if (!EnvironmentEx.IsRunningInContainer())
        {
            DotEnv.Load();
            builder.Configuration.AddEnvironmentVariables();
        }


        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(
            new NpgsqlDataSourceBuilder(
                    builder.Configuration[Config.Envs.Db.Connection]
                ).EnableDynamicJson()
                .Build()
        ));

        builder.Services.AddScoped<PurchasesService>();

        builder.Services.AddScoped<TransactionManager>();
        builder.Services.AddScoped<ProductsRepo>();
        builder.Services.AddSingleton<ProductMapper>();
        builder.Services.AddScoped<UsersRepo>();
        builder.Services.AddSingleton<UserMapper>();
        builder.Services.AddScoped<PurchasesRepo>();
        builder.Services.AddSingleton<PurchaseMapper>();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = "swagger";
                options.SwaggerEndpoint("/swagger/purchases/v1/swagger.json", "Purchases API");
            });
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        await app.RunAsync();
    }
}