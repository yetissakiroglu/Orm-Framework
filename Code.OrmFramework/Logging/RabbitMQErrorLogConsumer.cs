using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.OrmFramework.Logging
{
    public class RabbitMQErrorLogConsumer
    {
        private readonly string _errorQueueName = "ErrorQueue";
        private readonly ConnectionFactory _factory;

        public RabbitMQErrorLogConsumer(string rabbitMqHost)
        {
            _factory = new ConnectionFactory() { HostName = rabbitMqHost };
        }

        public void StartListening()
        {
            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var logEntry = JsonConvert.DeserializeObject<dynamic>(message);

                // Process the log
                Console.WriteLine($"Received log: {logEntry}");
                channel.BasicAck(ea.DeliveryTag, false);
            };

            channel.BasicConsume(queue: _errorQueueName, autoAck: false, consumer: consumer);
            Console.ReadLine();
        }
    }
}
