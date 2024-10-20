﻿using System.Reflection;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Petalaka.Payment.API.Base;
using Petalaka.Payment.API.ModelMappings;
using Petalaka.Payment.Repository.CustomSettings;
using Petalaka.Payment.Core.Utils;

namespace Petalaka.Payment.API;

public static class ConfigureService
{
    public static void AddConfigureServiceAPI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClientConfig();
        services.AddJwtSettings(configuration);
        services.AddAuthenticationJwt(configuration);
        services.ConfigRoute();
        services.AddCors();
        services.AddSwagger();
        services.AddSess();
        services.AddScoped<ValidateModelStateAttribute>();
        services.AddAutoMapperConfig(configuration);
    }
    public static void AddAutoMapperConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(PaymentMapping));
    }
    private static void AddSess(this IServiceCollection services)
    {
        services.AddDistributedMemoryCache();

        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromHours(1); // Thời gian hết hạn của session
            options.Cookie.HttpOnly = true; // Không cho phép JavaScript truy cập cookie
            options.Cookie.IsEssential = true; // Để sử dụng session cần thiết
        });
    }
    
    public static void ConfigRoute(this IServiceCollection services)
    {
        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
        });
    }
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo{ Title = "Payment API", Version = "v1" });
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter your valid token in the text input below.\n\nExample: \"yourTokenHere\"",
            });
            
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });
        
    }
    public static void AddCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader(); 
                });
        });
    }
    public static void AddJwtSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<JwtSettings>(options =>
        {
            JwtSettings jwtSettings = new JwtSettings
            {
                Key = configuration.GetSection("JwtSettings:Key").Value,
                Issuer = configuration.GetSection("JwtSettings:Issuer").Value,
                Audience = configuration.GetSection("JwtSettings:Audience").Value,
                AccessTokenExpirationMinutes =
                    Convert.ToInt32(configuration.GetSection("JwtSettings:AccessTokenExpiresInMinutes").Value),
                RefreshTokenExpirationHours = 
                    Convert.ToInt32(configuration.GetSection("JwtSettings:RefreshTokenExpiresInHours").Value)
            };
            jwtSettings.IsValid();
            return jwtSettings;
        });
    }

    public static void AddAuthenticationJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,    
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                ValidAudience = jwtSettings.GetSection("Audience").Value,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("Key").Value)),
                ClockSkew = TimeSpan.Zero // No tolerance for token expiration
            };
        });
    }

    public static void AddHttpClientConfig(this IServiceCollection services)
    {
        services.AddHttpClient();
    }
}