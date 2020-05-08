using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pi2000.Messaging.MessageDefinitions.P2000Alert.V1;
using Pi2000.Messaging.RabbitMQ;
using RabbitMQ.Client;

namespace Pi2000.Receiver.Mock
{
    internal class MockReceiverService : IHostedService, IDisposable
    {
        private readonly IRabbitMqConnectionFactory _connectionFactory;
        private readonly ILogger<MockReceiverService> _logger;
        private IConnection _connection;

        public MockReceiverService(IRabbitMqConnectionFactory connectionFactory, ILogger<MockReceiverService> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _connection = _connectionFactory.CreateConnection();
            _logger.LogInformation($"Succesfully started {nameof(MockReceiverService)}");

            return Task.Run(DoWork, cancellationToken);
        }

        private void DoWork()
        {
            Thread.Sleep(100);
            Console.WriteLine("Enter the raw alert messages line by line below. Press [Enter] to move to a new line. Press [Enter] again to send the alert.\n");
            
            while (true)
            {
                var l = 0;
                var alerts = new List<string>();
                do
                {
                    Console.Write($"{++l}: ");
                    var input = Console.ReadLine();
                    if (input == string.Empty) break;
                    alerts.Add(input);
                } while (true);

                Console.WriteLine($"SUBMIT\nAmount of capcodes: {alerts.Count}");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _connection.Close();
            _logger.LogInformation($"Succesfully stopped {nameof(MockReceiverService)}");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}
