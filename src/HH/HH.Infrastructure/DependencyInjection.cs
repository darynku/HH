using HH.Application.Files;
using HH.Application.UnitOfWork;
using HH.Domain.Interfaces.Repository;
using HH.Domain.Interfaces.Services;
using HH.Infrastructure.Database;
using HH.Infrastructure.Options;
using HH.Infrastructure.Providers;
using HH.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Minio;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
namespace HH.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastracture(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddRepositoriess()
                .AddServices()
                .AddDatabase(configuration)
                .AddJwtAuth(configuration)
                .AddStorage(configuration);


        return services;
    }
    public static IServiceCollection AddRepositoriess(this IServiceCollection services)
    {
        services.AddScoped<IVacancyRepository, VacancyRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }        
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IMinioProvider, MinioProvider>();
        return services;
    }        
    
    public static IServiceCollection AddJwtAuth(this IServiceCollection services, IConfiguration configuration) 
    {
        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
        
        services
            .AddAuthentication()
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                var key = configuration.GetSection(JwtOptions.Jwt).Get<JwtOptions>()
                                                    ?? throw new ApplicationException("Wrong configuration");

                var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key.Key));

                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = symmetricKey
                };

                options.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        //var accessToken = context.Request.Query["access_token"];
                        //var path = context.HttpContext.Request.Path;
                        //if(!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/chatHub"))
                        //{
                        //    context.Token = accessToken;
                        //}

                        context.Token = context.Request.Cookies["jwt"];
                    

                        return Task.CompletedTask;
                    }
                };
            });
        services.AddAuthorization();

        return services;
    }
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ApplicationDbContext>();

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
        });

        return services;
    }
    public static IServiceCollection AddStorage(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MinioOptions>(configuration.GetSection("Minio"));

        var minioOptions = configuration.GetSection("Minio").Get<MinioOptions>() ?? throw new ArgumentNullException("no minio configs");

        services.AddMinio(
            configClient =>
        {
            configClient
                .WithEndpoint("minio:9000")
                .WithCredentials("minioadmin", "minioadmin")
                .WithSSL(false)
                .Build();
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

}
