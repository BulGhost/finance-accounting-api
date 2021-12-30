using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;
using FinanceAccounting.Application;
using FinanceAccounting.Application.Abstractions.Security;
using FinanceAccounting.Application.Common.Mappings;
using FinanceAccounting.DataAccess;
using FinanceAccounting.DataAccess.DbContext;
using FinanceAccounting.Domain.Entities;
using FinanceAccounting.Security;
using FinanceAccounting.WebApi.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace FinanceAccounting.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetAssembly(typeof(AssemblyMappingProfile))));
            });

            services.AddApplication();
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDataAccess(connection);
            services.AddTransient<ExceptionHandlingMiddleware>();
            services.AddCors(); //TODO: Delete?
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FinanceAccounting.WebApi", Version = "v1" });
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
                    Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "Bearer"}
                };
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
                });
            });

            services.AddIdentity<User, IdentityRole<int>>(options =>
                    options.Password = new PasswordOptions
                    {
                        RequiredLength = 6,
                        RequireLowercase = true,
                        RequireUppercase = true,
                        RequireDigit = true,
                        RequireNonAlphanumeric = false
                    })
                .AddEntityFrameworkStores<BookkeepingDbContext>()
                .AddSignInManager<SignInManager<User>>()
                .AddUserManager<UserManager<User>>()
                .AddDefaultTokenProviders();

            var jwtConfig = new JwtConfig();
            Configuration.GetSection("Jwt").Bind(jwtConfig);
            var signingSymmetricKey = new SigningSymmetricKey(jwtConfig);
            services.AddSingleton<IJwtSigningEncodingKey>(signingSymmetricKey);
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IJwtVerifier, JwtVerifier>();

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtConfig.Issuer,
                ValidAlgorithms = new[] {jwtConfig.SigningAlgorithm},
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingSymmetricKey.GetKey(),
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
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FinanceAccounting.WebApi v1"));
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
