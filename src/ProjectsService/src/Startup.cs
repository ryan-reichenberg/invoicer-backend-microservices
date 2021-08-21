using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invoicer.Common.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            services.AddInvoicerCommon()
                .AddCqrs()
                .AddInMemoryCommandDispatcher()
                .AddInMemoryQueryDispatcher()
                .AddWebApi()
                .AddRabbitMq(plugins: p => p.AddJaegerRabbitMqPlugin())
                .AddJaeger()
                .Initialize();
            services.AddScoped<IProjectRepository, ProjectsRepository>();
            services.AddScoped<ITodoRepository, TodoRepository>();
            services.AddDbContext<TodoDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("TodoManagementCN")));
            services.AddDbContext<ProjectDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("ProjectManagementCN")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.RunInitializers();
            // app.UseRabbitMq()
            //     .SubscribeCommand<RegisterUserCommand>()
            //     .SubscribeEvent<UserRegisteredEvent>();

            app.UseRouting();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}