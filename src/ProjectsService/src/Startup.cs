using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.CQRS.Queries;
using Convey.MessageBrokers.CQRS;
using Convey.MessageBrokers.RabbitMQ;
using Convey.Tracing.Jaeger;
using Convey.Tracing.Jaeger.RabbitMQ;
using Convey.Types;
using Invoicer.Common.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ProjectsService.Repositories;

namespace ProjectsService
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
            services.AddControllers();
            services.AddScoped<IProjectRepository, ProjectsRepository>();
            services.AddDbContext<ProjectDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("ProjectManagementCN")));
            services.AddConvey()
                .AddCommandHandlers()
                .AddEventHandlers()
                .AddQueryHandlers()
                .AddInMemoryCommandDispatcher()
                .AddInMemoryQueryDispatcher()
                .AddRabbitMq(plugins: p => p.AddJaegerRabbitMqPlugin())
                .AddServiceBusEventDispatcher()
                .AddJaeger()
                .Build();

      
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            
            // app.UseRabbitMq()
            //     .SubscribeCommand<RegisterUserCommand>()
            //     .SubscribeEvent<UserRegisteredEvent>();

            app.UseRouting();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    var options = Configuration.GetOptions<AppOptions>("app");
                    await context.Response.WriteAsync($"{options.Name} v{options.Version}");
                });
                endpoints.MapControllers();
            });

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}