using System;
using Invoicer.Common.Messaging;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            services.UserRabbitMqPublisher(_configuration);
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddMediatR(typeof(Startup));
            services.AddDbContext<UserDbContext>(options => 
                options.UseSqlServer(_configuration.GetConnectionString("UserManagementCN")));
            services.AddMvc(options => options.EnableEndpointRouting = false)
                .AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();

            // auto migrate db
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                Console.WriteLine("Running migration");
                scope.ServiceProvider.GetService<UserDbContext>().MigrateDB();
            }
        }
        
    }
}
