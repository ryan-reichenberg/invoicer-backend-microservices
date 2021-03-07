using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Invoicer.Common.Extensions
{
    public static class InitializationContainerExtensions
    {
        public static TModel GetOptions<TModel>(this IInitializationContainer container, string settingsSectionName)
            where TModel : new()
        {
            using var serviceProvider = container.Services.BuildServiceProvider();
            var configuration = serviceProvider.GetService<IConfiguration>();
            return configuration.GetOptions<TModel>(settingsSectionName);
        }

    }
}