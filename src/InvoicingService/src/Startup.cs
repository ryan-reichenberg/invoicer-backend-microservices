using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.CQRS.Queries;
using Convey.MessageBrokers.RabbitMQ;
using Convey.Tracing.Jaeger;
using Convey.Tracing.Jaeger.RabbitMQ;
using Convey.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InvoicingService
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
            services.AddControllers();
            services.AddConvey()
                .AddCommandHandlers()
                .AddEventHandlers()
                .AddQueryHandlers()
                .AddInMemoryCommandDispatcher()
                .AddInMemoryQueryDispatcher()
                .AddRabbitMq(plugins: p => p.AddJaegerRabbitMqPlugin())
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
            
            // app.UseRabbitMq()
            //     .SubscribeCommand<RegisterUserCommand>()
            //     .SubscribeEvent<UserRegisteredEvent>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    var options = _configuration.GetOptions<AppOptions>("app");
                    await context.Response.WriteAsync($"{options.Name} v{options.Version}");
                });
                endpoints.MapControllers();
            });
        }
    }
}
