using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.OrmFramework.Events
{
    public class RabbitMQEventPublisher : IEventPublisher
    {
        private readonly string _queueName = "EventQueue";
        private readonly ConnectionFactory _factory;

        public RabbitMQEventPublisher(string rabbitMqHost)
        {
            _factory = new ConnectionFactory() { HostName = rabbitMqHost };
        }

        public void PublishEvent(string eventName, object eventData)
        {
            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new
            {
                Timestamp = DateTime.UtcNow,
                EventName = eventName,
                EventData = eventData
            }));

            channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
        }
    }
}
