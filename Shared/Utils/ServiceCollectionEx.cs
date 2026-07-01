using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Npgsql;
using Shared.Constants;
using Shared.Infrastructure;
using Shared.Users;

namespace Shared.Utils;

public static class ServiceCollectionEx
{
    extension(IServiceCollection services)
    {
        public void AddJwtAuthentication()
        {
            var secret = Environment.GetEnvironmentVariable(Config.Envs.Jwt.Secret)!;
            var issuer = Environment.GetEnvironmentVariable(Config.Envs.Jwt.Issuer)!;
            var audience = Environment.GetEnvironmentVariable(Config.Envs.Jwt.Audience)!;

            services.Configure<JwtSettings>(options =>
            {
                options.Secret = secret;
                options.Issuer = issuer;
                options.Audience = audience;
            });

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = issuer,
                        ValidateAudience = true,
                        ValidAudience = audience,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                        RoleClaimType = ClaimTypes.Role,
                        NameClaimType = ClaimTypes.NameIdentifier,
                    };
                });
        }

        public void AddSwaggerWithSecurityGen()
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference(JwtBearerDefaults.AuthenticationScheme, document)] = []
                });
            });
        }

        public void AddNpgsqlWithDynamicJson()
        {
            var dataSource = new NpgsqlDataSourceBuilder(Environment.GetEnvironmentVariable(Config.Envs.Db.Connection))
                .EnableDynamicJson()
                .Build();
            services.AddSingleton(dataSource);
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(dataSource));
        }
    }
}