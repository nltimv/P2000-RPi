using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pi2000.Messaging.RabbitMQ;
using Pi2000.Messaging.RabbitMQ.IOC;

namespace Pi2000.Receiver.Mock
{
    internal static class Program
    {
        internal static async Task Main(string[] args)
        {
            await new HostBuilder()
                
                .ConfigureServices(ConfigureServices)
                .ConfigureLogging(loggingBuilder =>
                {
                    loggingBuilder.AddConsole();
                    loggingBuilder.AddDebug();
                })
                .RunConsoleAsync();
        }

        private static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            services.AddLogging();
            services.ConfigureOptionsFromConfiguration();
            services.ConfigureRabbitMqConnection();

            services.AddHostedService<MockReceiverService>();
        }

        private static void ConfigureOptionsFromConfiguration(this IServiceCollection serviceCollection)
        {
            var configuration = GetConfiguration();
            serviceCollection.Configure<RabbitMqOptions>(configuration.GetSection("RabbitMq"));
        }

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder();
            builder.AddEnvironmentVariables("AppConfig_");
            return builder.Build();
        }
    }
}
