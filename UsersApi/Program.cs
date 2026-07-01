using dotenv.net;
using Microsoft.AspNetCore.Identity;
using Shared.Petitions;
using Shared.Users;
using Shared.Utils;
using UsersAPI.Auth;

namespace UsersApi;

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

        builder.Services.AddScoped<UsersRepo>();
        builder.Services.AddScoped<CreateSellerPetitionsRepo>();
        builder.Services.AddSingleton<UserMapper>();
        builder.Services.AddSingleton<CreateSellerPetitionMapper>();
        
        
        builder.Services.AddScoped<AuthService>();

        builder.Services.AddSingleton<PasswordHasher<UserModel>>();

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