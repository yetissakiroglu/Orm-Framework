using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.OrmFramework.Logging
{

    public class RabbitMQLoggerService : ILoggerService
    {
        private readonly string _queueName = "LogQueue";
        private readonly ConnectionFactory _factory;

        public RabbitMQLoggerService(string rabbitMqHost)
        {
            _factory = new ConnectionFactory() { HostName = rabbitMqHost };
        }

        public void LogInfo(string actionName, object request, object response)
        {
            SendToQueue("Info", actionName, request, response);
        }

        public void LogError(string actionName, object request, Exception ex)
        {
            var errorMessage = $"{ex.Message} - Exception: {ex.StackTrace}";
            SendToQueue("Error", actionName, request, errorMessage);
        }

        private void SendToQueue(string logType, string actionName, object request, object response)
        {
            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new
            {
                Timestamp = DateTime.UtcNow,
                LogType = logType,
                ActionName = actionName,
                Request = request,
                Response = response
            }));

            channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
        }
    }
}
