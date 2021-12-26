
using System;
using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.CQRS.Queries;
using Convey.MessageBrokers.RabbitMQ;
using Convey.Tracing.Jaeger.RabbitMQ;
using Invoicer.Common.CQRS.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserService.Logging;
using UserService.Messages.Commands;
using UserService.Repositories;

namespace UserService
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = typeof(RegisterUserCommand).Assembly;
            
            var builder = services
                .AddConvey();
            builder.Services.AddOpenTracing();
            builder.Services.AddControllers();
            builder.Services.AddSingleton<IMessageToLogTemplateMapper>(new MessageToLogTemplateMapper());
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddDbContext<UserDbContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("UserManagementCN")));
            builder
                .AddCommandHandlers()
                .AddQueryHandlers()
                .AddEventHandlers()
                // .AddQueryHandlersLogging()
                .AddInMemoryCommandDispatcher()
                .AddInMemoryQueryDispatcher()
                .AddCommandHandlersLogging(assembly)
                .AddEventHandlersLogging(assembly)
                .AddQueryHandlersLogging(assembly)
                .AddRabbitMq(plugins: p => p.AddJaegerRabbitMqPlugin());

            builder.Build();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            
            // app.UseRabbitMq()
            //     .SubscribeCommand<>

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            // auto migrate db
            using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            Console.WriteLine("Running migration");
            scope.ServiceProvider.GetService<UserDbContext>()?.MigrateDB();
        }
        
    }
}
