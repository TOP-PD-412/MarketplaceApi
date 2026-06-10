using Microsoft.EntityFrameworkCore;
using ProductsApi.Core.Infrastructure.Db.Mappers;
using ProductsApi.Modules.Products.Db.Entities;
using ProductsApi.Modules.Products.Db.Mappers;
using ProductsApi.Modules.Products.Db.Repos;
using ProductsApi.Modules.Products.Domain.Models;
using ProductsApi.Modules.Products.Services;
using ProductsApi.Modules.Shared.Db;

namespace ProductsApi;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<ProductsDbContext>(options => options.UseNpgsql(
            builder.Configuration["PRODUCTS_DB_CONNECTION_STRING"])
        );

        #region Services

        builder.Services.AddScoped<IProductsService, ProductsService>();

        #endregion

        #region Repos

        builder.Services.AddScoped<IProductsRepo, ProductsRepo>();

        #endregion

        #region Mappers

        builder.Services.AddSingleton<IMapper<ProductModel, ProductEntity>, ProductMapper>();

        #endregion

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        await app.RunAsync();
    }
}