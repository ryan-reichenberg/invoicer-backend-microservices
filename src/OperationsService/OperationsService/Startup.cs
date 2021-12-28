
using Convey;
using Convey.Auth;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.CQRS.Queries;
using Convey.MessageBrokers.RabbitMQ;
using Convey.Persistence.MongoDB;
using Convey.Persistence.Redis;
using Convey.Security;
using Convey.Tracing.Jaeger;
using Convey.Tracing.Jaeger.RabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OperationsService.Extentions;
using OperationsService.Hubs;
using OperationsService.Messages.Handlers;
using OperationsService.Messages.Subscribers;
using OperationsService.Services;
using OperationsService.Types;

namespace OperationsService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = services.AddConvey();
            var requestsOptions = builder.GetOptions<RequestsOptions>("requests");
            builder.Services.AddSingleton(requestsOptions);
            builder.Services.AddTransient<ICommandHandler<ICommand>, GenericCommandHandler<ICommand>>()
                .AddTransient<IEventHandler<IEvent>, GenericEventHandler<IEvent>>()
                .AddTransient<IEventHandler<IRejectedEvent>, GenericRejectedEventHandler<IRejectedEvent>>()
                .AddTransient<IHubService, HubService>()
                .AddTransient<IHubWrapper, HubWrapper>()
                .AddSingleton<IOperationsService, Services.OperationsService>();
            //builder.Services.AddGrpc();
            
            builder
                .AddJwt()
                .AddCommandHandlers()
                .AddEventHandlers()
                .AddQueryHandlers()
                .AddRabbitMq(plugins: p => p.AddJaegerRabbitMqPlugin())
                .AddRedis()
               // .AddMetrics()
                .AddJaeger()
                .AddSignalR()
                .AddSecurity()
                .Build();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseJaeger()
                .UseConvey()
                .UseStaticFiles()
                .UseRabbitMq()
                .SubscribeMessages();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<InvoicerHub>("/hub");
                //endpoints.MapGrpcService<GrpcServiceHost>();
            });
        }
    }
}