﻿using System;
using System.Reflection;
using FinanceAccounting.Application.Abstractions.Repo;
using FinanceAccounting.Application.Abstractions.Security;
using FinanceAccounting.Application.Common.Models;
using FinanceAccounting.DataAccess.DbContext;
using FinanceAccounting.Domain.Entities;
using FinanceAccounting.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;

namespace FinanceAccounting.WebApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                string appVersion = Assembly.GetExecutingAssembly().GetName().Version?.Major.ToString();
                c.SwaggerDoc($"v{appVersion}", new OpenApiInfo { Title = "Finance Accounting API", Version = $"v{appVersion}" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Type = SecuritySchemeType.ApiKey
                });

                var securityScheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                };
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
                });
            });

            return services;
        }

        public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
        {
            services.AddIdentityCore<User>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.Password = new PasswordOptions
                    {
                        RequiredLength = 6,
                        RequireLowercase = true,
                        RequireUppercase = true,
                        RequireDigit = true,
                        RequireNonAlphanumeric = false
                    };
                })
                .AddEntityFrameworkStores<BookkeepingDbContext>()
                .AddUserManager<UserManager<User>>();

            return services;
        }

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfig = new AuthenticationConfig();
            configuration.GetSection("Jwt").Bind(jwtConfig);
            Func<string, SecurityKey> getSigningKeyFunc = SigningSymmetricKey.GetKey;
            services.AddScoped<ITokenGenerator>(provider => new TokenGenerator(
                jwtConfig, getSigningKeyFunc, provider.GetService<IRefreshTokenRepo>()));
            services.AddScoped<ITokenValidator, TokenValidator>();

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtConfig.Issuer,
                ValidAlgorithms = new[] { jwtConfig.SigningAlgorithm },
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = getSigningKeyFunc(jwtConfig.AccessTokenSecret),
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(option =>
                {
                    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = tokenValidationParameters;
                    options.MapInboundClaims = false;
                });

            return services;
        }
    }
}
