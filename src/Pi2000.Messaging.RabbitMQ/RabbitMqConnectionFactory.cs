using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Pi2000.Messaging.RabbitMQ
{
    internal class RabbitMqConnectionFactory : IRabbitMqConnectionFactory
    {
        private readonly RabbitMqOptions _connectionOptions;
        private readonly ILogger<RabbitMqConnectionFactory> _logger;

        public RabbitMqConnectionFactory(IOptions<RabbitMqOptions> connectionOptions, ILoggerFactory loggerFactory)
        {
            _connectionOptions = connectionOptions.Value;
            _logger = loggerFactory.CreateLogger<RabbitMqConnectionFactory>();
        }

        public IConnection CreateConnection()
        {
            if (_connectionOptions == null) throw new ArgumentNullException(nameof(_connectionOptions));

            var uriParsed = Uri.TryCreate(_connectionOptions.ConnectionString, UriKind.Absolute, out var connectionUri);

            if (!uriParsed || connectionUri.Scheme != "amqp")
            {
                _logger.LogError($"RabbitMQ connection string is in an invalid format! {_connectionOptions.ConnectionString}");
                throw new UriFormatException($"RabbitMQ connection string is in an invalid format!");
            }

            var connectionFactory = new ConnectionFactory
            {
                Uri = connectionUri
            };

            var connection = connectionFactory.CreateConnection();
            _logger.LogInformation("RabbitMQ connection successfully created!");

            return connection;
        }
    }
}
