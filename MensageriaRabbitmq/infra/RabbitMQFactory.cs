using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using MensageriaRabbitmq.infra.Interfaces;

namespace MensageriaRabbitmq.infra
{
    public class RabbitMQFactory : IRabbitMQFactory
    {
        private ConnectionFactory connectionFactory;
        public RabbitMQFactory()
        {

                connectionFactory = new ConnectionFactory() { HostName = "host.docker.internal" };   
        }

        public void SendMessage(string queueName,object content)
        {

            using(var connection = connectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queueName,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var contentString = JsonConvert.SerializeObject(content);
                    var body = Encoding.UTF8.GetBytes(contentString);

                    channel.BasicPublish(exchange: string.Empty,
                                         routingKey: queueName,
                                         basicProperties: null,
                                         body: body);
                }
            }
        }
    }
}
