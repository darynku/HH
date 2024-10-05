using HH.Application.Files;
using HH.Application.Messaging;
using HH.Application.UnitOfWork;
using HH.Infrastructure.Database;
using HH.Infrastructure.Options;
using HH.Infrastructure.Providers;
using HH.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Minio;
using FileInfo = HH.Application.Files.FileInfo;
using System.Text;
using HH.Infrastructure.MessageQueues;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using StackExchange.Redis;
using Microsoft.AspNetCore.Authorization;
using HH.Application.Features.Users;
using HH.Application.Features.Vacancies;
using HH.Domain.Interfaces;
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

        services.AddSingleton<IMessageQueue<IEnumerable<FileInfo>>, MessageQueue<IEnumerable<FileInfo>>>();
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
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
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
                .WithEndpoint(minioOptions.Endpoint)
                .WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey)
                .WithSSL(false)
                .Build();
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

}
