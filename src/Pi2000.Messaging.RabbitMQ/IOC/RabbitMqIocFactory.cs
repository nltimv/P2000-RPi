using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Pi2000.Messaging.RabbitMQ.IOC
{
    public static class RabbitMqIocFactory
    {
        public static void ConfigureRabbitMqConnection(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IRabbitMqConnectionFactory, RabbitMqConnectionFactory>();
        }
    }
}
