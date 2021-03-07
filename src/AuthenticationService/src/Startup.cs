using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Extensions;
using AuthenticationService.Providers;
using AuthenticationService.Repositories;
using AuthenticationService.Repositories.Documents;
using AuthenticationService.Services;
using Invoicer.Common.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AuthenticationService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IJwtProvider, JwtProvider>();
            services.AddSingleton<IPasswordService, PasswordService>();
            services.AddSingleton<IPasswordHasher<IPasswordService>, PasswordHasher<IPasswordService>>();
            services.AddTransient<IRefreshTokenService, RefreshTokenService>();
            services.AddTransient<IAuthenticationService, Services.AuthenticationService>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddInvoicerCommon()
                .AddWebApi()
                .AddRabbitMq(plugins: p => p.AddJaegerRabbitMqPlugin())
                .AddJaeger()
                .AddJwt()
                .AddMongo()
                .AddMongoRepository<RefreshTokenDocument, Guid>("refreshTokens")
                .AddMongoRepository<UserDocument, Guid>("users")
                .Initialize();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.RunInitializers();
            app.UseHttpsRedirection();
            app.UseJaeger();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Authentication Service API v1.0");
                });
                endpoints.MapControllers();
            });
        }
    }
}
