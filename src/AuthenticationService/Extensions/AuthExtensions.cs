using System;
using System.Text;
using AuthenticationService.Handlers;
using AuthenticationService.Repositories;
using AuthenticationService.Services;
using Invoicer.Common.Authentication;
using Invoicer.Common.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationService.Extensions
{
    public static class AuthExtensions
    {
        private static readonly string SectionName = "jwt";
        
        public static void AddJwt(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }
            var section = configuration?.GetSection(SectionName);
            var options = configuration.GetOptions<JwtOptions>(SectionName);
            services.Configure<JwtOptions>(section);
            services.AddSingleton(options);
            services.AddSingleton<IJwtHandler, JwtHandler>();
            services.AddTransient<IAuthenticationService, Services.AuthenticationService>();
            services.AddTransient<IRefreshTokenService, RefreshTokenService>();
            services.AddTransient<IAccessTokenService, AccessTokenService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddAuthentication()
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)),
                        ValidIssuer = options.Issuer,
                        ValidAudience = options.ValidAudience,
                        ValidateAudience = options.ValidateAudience,
                        ValidateLifetime = options.ValidateLifetime,
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }
    }
}