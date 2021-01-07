using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Invoicer.Common.Extensions
{
    public static class InitializersExtensions
    {
        public static IServiceCollection AddInvoicerCommon(this IServiceCollection services)
        {
            services.AddSingleton<IStartupInitializer>(new StartupInitializer());
            return services;
        }

        public static IApplicationBuilder RunInitializers(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var initializer = scope.ServiceProvider.GetRequiredService<IStartupInitializer>();
                Task.Run(() => initializer.InitializeAsync()).GetAwaiter().GetResult();
                return app;
            }
        }
    }
}