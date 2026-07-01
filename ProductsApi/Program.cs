using dotenv.net;
using ProductsApi.Products;
using Shared.Products;
using Shared.Utils;

namespace ProductsAPI;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        if (!EnvironmentEx.IsRunningInContainer)
        {
            DotEnv.Load();
            builder.Configuration.AddEnvironmentVariables();
        }

        builder.Services.AddControllers();
        builder.Services.AddJwtAuthentication();
        builder.Services.AddAuthorization();
        builder.Services.AddNpgsqlWithDynamicJson();

        builder.Services.AddOpenApi();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerWithSecurityGen();

        builder.Services.AddScoped<ProductsService>();
        builder.Services.AddScoped<ProductsRepo>();
        builder.Services.AddSingleton<ProductMapper>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        await app.RunAsync();
    }
}