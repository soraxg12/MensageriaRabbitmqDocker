namespace MensageriaRabbitmq.infra.Interfaces
{
    public interface IRabbitMQFactory
    {
        void SendMessage(string queueName, object content);
    }
}
