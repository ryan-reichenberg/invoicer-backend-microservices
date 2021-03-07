using System;
using System.Threading.Tasks;
using Invoicer.Common.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Invoicer.Common.Extensions
{
    public static class InitializersExtensions
    {
        private const string SectionName = "app";
        public static IInitializationContainer AddInvoicerCommon(this IServiceCollection services, string sectionName = SectionName)
        {
            if (string.IsNullOrWhiteSpace(sectionName))
            {
                sectionName = SectionName;
            }

            var builder = InitializationContainer.Create(services);
            var options = builder.GetOptions<AppOptions>(sectionName);
            builder.Services.AddMemoryCache();
            services.AddSingleton(options);
            if (!options.DisplayBanner || string.IsNullOrWhiteSpace(options.Name))
            {
                return builder;
            }

            var version = options.DisplayVersion ? $" {options.Version}" : string.Empty;
            Console.WriteLine(Figgle.FiggleFonts.Doom.Render($"{options.Name} v{version}"));

            return builder;
        }


        public static IApplicationBuilder RunInitializers(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var initializer = scope.ServiceProvider.GetRequiredService<IStartupInitializer>();
                Task.Run(() => initializer.InitializeAsync()).GetAwaiter().GetResult();
            }

            return app;
        }

    }
}