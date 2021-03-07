using System;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Invoicer.Common.Handlers;
using Invoicer.Common.RabbitMq;
using Invoicer.Common.RabbitMq.Contexts;
using Invoicer.Common.RabbitMq.Conventions;
using Invoicer.Common.RabbitMq.Plugins;
using Invoicer.Common.RabbitMq.Publishers;
using Invoicer.Common.RabbitMq.Serializers;
using Invoicer.Common.RabbitMq.Subscribers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Invoicer.Common.Extensions
{
    public static class RabbitMqExtensions
    {
        private const string SectionName = "rabbitmq";
        private const string RegistryName = "messageBrokers.rabbitmq";

        public static IInitializationContainer AddRabbitMq(this IInitializationContainer container, string sectionName = SectionName,
            Func<IRabbitMqPluginsRegistry, IRabbitMqPluginsRegistry> plugins = null,
            Action<ConnectionFactory> connectionFactoryConfigurator = null)
        {
            if (string.IsNullOrWhiteSpace(sectionName))
            {
                sectionName = SectionName;
            }
            var options = container.GetOptions<RabbitMqOptions>(sectionName);
            container.Services.AddSingleton(options);
            if (!container.TryRegister(RegistryName))
            {
                return container;
            }

            if (options.HostNames is null || !options.HostNames.Any())
            {
                throw new ArgumentException("RabbitMQ hostnames are not specified.", nameof(options.HostNames));
            }


            ILogger<IRabbitMqClient> logger;
            using (var serviceProvider = container.Services.BuildServiceProvider())
            {
               logger = serviceProvider.GetService<ILogger<IRabbitMqClient>>();
            }

            container.Services.AddSingleton<IContextProvider, ContextProvider>();
            container.Services.AddSingleton<ICorrelationContextAccessor>(new CorrelationContextAccessor());
            container.Services.AddSingleton<IMessagePropertiesAccessor>(new MessagePropertiesAccessor());
            container.Services.AddSingleton<IConventionsBuilder, ConventionsBuilder>();
            container.Services.AddSingleton<IConventionsProvider, ConventionsProvider>();
            container.Services.AddSingleton<IConventionsRegistry, ConventionsRegistry>();
            container.Services.AddSingleton<IRabbitMqSerializer, RabbitMqSerializer>();
            container.Services.AddSingleton<IRabbitMqClient, RabbitMqClient>();
            container.Services.AddSingleton<IBusPublisher, RabbitMqPublisher>();
            container.Services.AddSingleton<IBusSubscriber, RabbitMqSubscriber>();
            container.Services.AddTransient<RabbitMqExchangeInitializer>();
            container.Services.AddHostedService<RabbitMqHostedService>();
            container.AddInitializer<RabbitMqExchangeInitializer>();

            var pluginsRegistry = new RabbitMqPluginsRegistry();
            container.Services.AddSingleton<IRabbitMqPluginsRegistryAccessor>(pluginsRegistry);
            container.Services.AddSingleton<IRabbitMqPluginsExecutor, RabbitMqPluginsExecutor>();
            plugins?.Invoke(pluginsRegistry);

            var connectionFactory = new ConnectionFactory
            {
                Port = options.Port,
                VirtualHost = options.VirtualHost,
                UserName = options.Username,
                Password = options.Password,
                RequestedHeartbeat = options.RequestedHeartbeat,
                RequestedConnectionTimeout = options.RequestedConnectionTimeout,
                SocketReadTimeout = options.SocketReadTimeout,
                SocketWriteTimeout = options.SocketWriteTimeout,
                RequestedChannelMax = options.RequestedChannelMax,
                RequestedFrameMax = options.RequestedFrameMax,
                UseBackgroundThreadsForIO = options.UseBackgroundThreadsForIO,
                DispatchConsumersAsync = true,
                ContinuationTimeout = options.ContinuationTimeout,
                HandshakeContinuationTimeout = options.HandshakeContinuationTimeout,
                NetworkRecoveryInterval = options.NetworkRecoveryInterval,
                Ssl = options.Ssl is null
                    ? new SslOption()
                    : new SslOption(options.Ssl.ServerName, options.Ssl.CertificatePath, options.Ssl.Enabled)
            };
            ConfigureSsl(connectionFactory, options, logger);
            connectionFactoryConfigurator?.Invoke(connectionFactory);

            logger.LogDebug($"Connecting to RabbitMQ: '{string.Join(", ", options.HostNames)}'...");
            var connection = connectionFactory.CreateConnection(options.HostNames.ToList(), options.ConnectionName);
            logger.LogDebug($"Connected to RabbitMQ: '{string.Join(", ", options.HostNames)}'.");
            container.Services.AddSingleton(connection);

            ((IRabbitMqPluginsRegistryAccessor) pluginsRegistry).Get().ToList().ForEach(p =>
                container.Services.AddTransient(p.PluginType));

            return container;
        }

        private static void ConfigureSsl(ConnectionFactory connectionFactory, RabbitMqOptions options,
            ILogger<IRabbitMqClient> logger)
        {
            if (options.Ssl is null || string.IsNullOrWhiteSpace(options.Ssl.ServerName))
            {
                connectionFactory.Ssl = new SslOption();
                return;
            }

            connectionFactory.Ssl = new SslOption(options.Ssl.ServerName, options.Ssl.CertificatePath,
                options.Ssl.Enabled);

            logger.LogDebug($"RabbitMQ SSL is: {(options.Ssl.Enabled ? "enabled" : "disabled")}, " +
                            $"server: '{options.Ssl.ServerName}', client certificate: '{options.Ssl.CertificatePath}', " +
                            $"CA certificate: '{options.Ssl.CaCertificatePath}'.");

            if (string.IsNullOrWhiteSpace(options.Ssl.CaCertificatePath))
            {
                return;
            }

            connectionFactory.Ssl.CertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) =>
            {
                if (sslPolicyErrors == SslPolicyErrors.None)
                {
                    return true;
                }

                if (chain is null)
                {
                    return false;
                }

                chain = new X509Chain();
                var certificate2 = new X509Certificate2(certificate);
                var signerCertificate2 = new X509Certificate2(options.Ssl.CaCertificatePath);
                chain.ChainPolicy.ExtraStore.Add(signerCertificate2);
                chain.Build(certificate2);
                var ignoredStatuses = Enumerable.Empty<X509ChainStatusFlags>();
                if (options.Ssl.X509IgnoredStatuses?.Any() is true)
                {
                    logger.LogDebug("Ignored X509 certificate chain statuses: " +
                                    $"{string.Join(", ", options.Ssl.X509IgnoredStatuses)}.");
                    ignoredStatuses = options.Ssl.X509IgnoredStatuses
                        .Select(s => Enum.Parse<X509ChainStatusFlags>(s, true));
                }

                var statuses = chain.ChainStatus.ToList();
                logger.LogDebug("Received X509 certificate chain statuses: " +
                                $"{string.Join(", ", statuses.Select(x => x.Status))}");

                var isValid = statuses.All(chainStatus => chainStatus.Status == X509ChainStatusFlags.NoError
                                                          || ignoredStatuses.Contains(chainStatus.Status));
                if (!isValid)
                {
                    logger.LogError(string.Join(Environment.NewLine,
                        statuses.Select(s => $"{s.Status} - {s.StatusInformation}")));
                }

                return isValid;
            };
        }

        public static IInitializationContainer AddExceptionToMessageMapper<T>(this IInitializationContainer container)
            where T : class, IExceptionToMessageMapper
        {
            container.Services.AddSingleton<IExceptionToMessageMapper, T>();

            return container;
        }

        public static IBusSubscriber UseRabbitMq(this IApplicationBuilder app)
            => new RabbitMqSubscriber(app.ApplicationServices);
        
        public static Task SendAsync<TCommand>(this IBusPublisher busPublisher, TCommand command, object messageContext)
            where TCommand : class, ICommand
            => busPublisher.PublishAsync(command, messageContext: messageContext);

        public static Task PublishAsync<TEvent>(this IBusPublisher busPublisher, TEvent @event, object messageContext)
            where TEvent : class, IEvent
            => busPublisher.PublishAsync(@event, messageContext: messageContext);

        public static IBusSubscriber SubscribeCommand<T>(this IBusSubscriber busSubscriber) where T : class, ICommand
            => busSubscriber.Subscribe<T>(async (serviceProvider, command, _) =>
            {
                using var scope = serviceProvider.CreateScope();
                await scope.ServiceProvider.GetRequiredService<ICommandHandler<T>>().HandleAsync(command);
            });

        public static IBusSubscriber SubscribeEvent<T>(this IBusSubscriber busSubscriber) where T : class, IEvent
            => busSubscriber.Subscribe<T>(async (serviceProvider, @event, _) =>
            {
                using var scope = serviceProvider.CreateScope();
                await scope.ServiceProvider.GetRequiredService<IEventHandler<T>>().HandleAsync(@event);
            });
    }
}