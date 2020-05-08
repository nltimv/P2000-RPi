using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace Pi2000.Messaging.RabbitMQ
{
    public interface IRabbitMqConnectionFactory
    {
        IConnection CreateConnection();
    }
}
