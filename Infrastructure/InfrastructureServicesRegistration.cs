using Domain;
using Persistence;
using System.Text;
using Application.Contracts;
using System.Security.Claims;
using Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Application.Contracts.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>();
            services.AddAuthentication(
                opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        RequireExpirationTime = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        RoleClaimType = ClaimTypes.Role,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(configuration["Jwt:SecurityKey"])
                        )
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];
                            var path = context.HttpContext.Request.Path;

                            if ((!string.IsNullOrEmpty(accessToken)) && (path.StartsWithSegments("/notifications")))
                            {
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                }
            );
            services.AddAuthorization();

            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserAccessor, UserAccessor>();
            // services.AddScoped<IEthereumCryptoService, EthereumCryptoService>();
            // services.AddScoped<IAuctionManagementService, AuctionManagementService>();
            // services.AddScoped<IEmbeddingService, OpenAiEmbeddingService>((provider) => new OpenAiEmbeddingService(
            //     configuration["OpenAi:API_KEY"],
            //     configuration["OpenAi:MODEL"]
            // ));
            // services.AddScoped<IEmbeddingService, BedrockEmbeddingService>(provider => new BedrockEmbeddingService(
            //     configuration["Amazon-AWS:REGION"],
            //     configuration["Amazon-AWS:MODEL_ID"],
            //     configuration["Amazon-AWS:ACCESS_KEY"],
            //     configuration["Amazon-AWS:SECRET_KEY"],
            //     provider.GetService<ILogger<BedrockEmbeddingService>>()
            // ));
            // services.AddScoped<ISemanticSearchService, SemanticSearchService>();


            services.AddLogging(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Debug);
                builder.AddConsole();
            });

            services.AddSignalR();
            // services.AddScoped<INotificationService, NotificationService>();

            return services;
        }
    }
}
