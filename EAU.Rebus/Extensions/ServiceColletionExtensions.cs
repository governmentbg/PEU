using CNSys.Data;
using EAU.Common;
using EAU.Data;
using EAU.Payments.Obligations.MessageHandlers;
using EAU.Rebus;
using EAU.Services.MessageHandlers;
using EAU.Signing.ReMessageHandlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Rebus.Config;
using Rebus.Retry.Simple;
using Rebus.Routing.TypeBased;
using Rebus.ServiceProvider;
using Rebus.SqlServer;
using System;
using static Rebus.Routing.TypeBased.TypeBasedRouterConfigurationExtensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionCommonExtensions
    {
        public const string PortalQueue = "[rebus].[qt_portal_api]";
        public const string PortalErrorQueue = "[rebus].[qt_portal_api_errors]";

        /// <summary>
        /// Добавя инфраструкрутата за основната опашка в портала. Тук се регистрират всички хендлъри и типове, които трябва да се мапват. 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEAUPortalQueue(this IServiceCollection services, IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            string tmp_portalQueue = null;
            string tmp_portalErrorQueue = null;

            var eauConfig = configuration.GetEAUSection();

            tmp_portalQueue = eauConfig.GetValue<string>("PORTAL_QUEUE") ?? PortalQueue;
            tmp_portalErrorQueue = eauConfig.GetValue<string>("PORTAL_ERROR_QUEUE") ?? PortalErrorQueue;

            bool evroTrustCallbackEnabled = eauConfig.GetValue<bool>("EVROTRUST_PROCESS_CALLBACK_ENABLED");

            /*променяме имената на опашките, ако средата е Development.Local и е вдигнат флага QUEUES_ENABLE_LOCAL*/
            ChangeNamesForLocalQueues(hostEnvironment, configuration, ref tmp_portalQueue, ref tmp_portalErrorQueue);

            /*
             при нужда от отделна опашка (за сега е само една), ще се отдели в отделно assembly, така че да не се реферират ненужни функционалности.
             */
            services.AddEAURebus(configuration, inputQueue: tmp_portalQueue, errorQueue: tmp_portalErrorQueue, builder =>
            {
                builder.Map<SigningProcessCompleteMessage>(tmp_portalQueue)
                    .Map<ObligationProcessMessage>(tmp_portalQueue)
                    .Map<SigningProcessRejectMessage>(tmp_portalQueue)
                    .Map<DocumentSendMessage>(tmp_portalQueue);

                if (evroTrustCallbackEnabled)
                {
                    builder.Map<EvrotrustProcessCallbackNotificationMessage>(tmp_portalQueue);
                }
            });

            services.AutoRegisterHandlersFromAssemblyOf<SigningProcessCompleteMessageHandler>();
            services.AutoRegisterHandlersFromAssemblyOf<ObligationProcessMessageHandler>();
            services.AutoRegisterHandlersFromAssemblyOf<DocumentSendMessage>();

            return services;
        }

        public static IServiceCollection AddEAURebus(
            this IServiceCollection services, 
            IConfiguration configuration,
            string inputQueue, 
            string errorQueue,
            Action<TypeBasedRouterConfigurationBuilder> messageConfiguration)
        {
            services.AddEAUPaymentsServices();

            services.TryAddSingleton<IActionDispatcher, DefaultActionDispatcher>();

            services.TryAddSingleton<IAmbientDbConnectionAccessor, AmbientDbConnectionAccessor>();

            services.TryAddSingleton<SqlConnetionProider>();

            services.AddRebusHandler<FailedMessageHandler>();

            services.ConfigureEAU<FailedMessageOptions>(configuration);

            services.AddRebus((rconfig, sp) =>
            {
                rconfig.Logging((lconfig) => lconfig.MicrosoftExtensionsLogging(sp.GetRequiredService<ILoggerFactory>()));

                var transportOptions = new SqlServerTransportOptions(sp.GetRequiredService<SqlConnetionProider>());

                var he = sp.GetRequiredService<IHostEnvironment>();
                /*когато се работи в локална среда, може да се създават таблиците за опашките!*/
                transportOptions.SetEnsureTablesAreCreated(he.IsDevelopmentLocal());
                transportOptions.SetAutoDeleteQueue(false);

                rconfig.Transport(t => t.UseSqlServer(transportOptions, inputQueue));

                rconfig.Routing(r =>
                {
                    var builder = r.TypeBased();
                    messageConfiguration?.Invoke(builder);
                });

                /*за maxDeliveryAttempts слагаме 1, защото се ползват повторните опити от второ ниво*/
                rconfig.Options(conf => conf.SimpleRetryStrategy(errorQueueAddress: errorQueue, maxDeliveryAttempts: 1, secondLevelRetriesEnabled: true));

                return rconfig;
            });

            return services;
        }

        public static void ChangeNamesForLocalQueues(IHostEnvironment hostEnvironment, IConfiguration configuration, ref string inputQueue, ref string errorQueue)
        {
            if (hostEnvironment.IsDevelopmentLocal() &&
                string.Compare(configuration.GetEAUSection().GetValue<string>("QUEUES_ENABLE_LOCAL"), "true", true) == 0)
            {
                var hostName = System.Net.Dns.GetHostName().ToLower();

                var inQ = TableName.Parse(inputQueue);
                var errQ = TableName.Parse(errorQueue);

                inQ = new TableName(inQ.Schema, $"{hostName}_{inQ.Name}");
                errQ = new TableName(inQ.Schema, $"{hostName}_{errQ.Name}");

                inputQueue = inQ.ToString();
                errorQueue = errQ.ToString();
            }
        }
    }
}
