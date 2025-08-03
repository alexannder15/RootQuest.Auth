using System.Text;
using Application.Exceptions;
using Application.Interfaces;
using Domain.AggregateRoots;
using Domain.Interfaces;
using Domain.Models.Identity;
using Infrastructure.Context;
using Infrastructure.Email;
using Infrastructure.Interfaces;
using Infrastructure.Messaging;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureCustomServices(
        this IServiceCollection services
    )
    {
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }

    public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
    {
        //services.AddDbContext<AppDbContext>(options =>
        //    options.UseNpgsql(configuration.GetConnectionString("SqlDataBase"))
        //);

        services
            .AddIdentityCore<User>()
            .AddRoles<Role>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        //.AddDefaultUI()

        return services;
    }

    public static IServiceCollection AddCustomAuthenticationJwt(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services
            .AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                var secret = configuration.GetSection("JwtConfig:Secret").Value;
                var issuer = configuration.GetSection("JwtConfig:Issuer").Value;
                var audience = configuration.GetSection("JwtConfig:Audience").Value;

                if (secret == null || issuer == null || audience == null)
                    throw new UnhandledException("Something was wrong with AddJwtBearer JwtConfig");

                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true, // for dev
                    ValidateAudience = true, // for dev
                    RequireExpirationTime = true, // for dev
                    ValidateLifetime = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret)),
                };
            });

        return services;
    }

    public static IServiceCollection AddCustomSendGrid(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.Configure<SendGridSettings>(configuration.GetSection("SendGrid"));
        services.AddScoped<IEmailService, SendGridEmailService>();

        return services;
    }

    public static IServiceCollection AddCustomRabbitMQ(this IServiceCollection services)
    {
        services.AddSingleton<IRabbitMqPublisher, RabbitMqPublisher>();

        return services;
    }
}
