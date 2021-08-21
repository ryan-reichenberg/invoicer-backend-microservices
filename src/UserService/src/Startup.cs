using Invoicer.Common.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserService.Events;
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
            // We only need publisher here
            services.AddInvoicerCommon()
                .AddCqrs()
                .AddInMemoryCommandDispatcher()
                .AddInMemoryQueryDispatcher()
                .AddWebApi()
                .AddRabbitMq(plugins: p => p.AddJaegerRabbitMqPlugin())
                .AddJaeger()
                .Initialize();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddDbContext<UserDbContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("UserManagementCN")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.RunInitializers();
            app.UseRabbitMq()
                .SubscribeCommand<RegisterUserCommand>()
                .SubscribeEvent<UserRegisteredEvent>();

            app.UseRouting();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            // auto migrate db
            // using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            // Console.WriteLine("Running migration");
            // scope.ServiceProvider.GetService<UserDbContext>().MigrateDB();
        }
        
    }
}
