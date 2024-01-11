using BlazorSozluk.Common.Constants;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace BlazorSozluk.Common.Infrasturucture.ConsumerFactory;

public static class QueueFactory
{
    public static void SendMessageToExchange(string exchangeName, string exchangeType, string queueName, object obj)
    {
        var channel = CreateBasicConsumer().EnsureExchange(exchangeName, exchangeType).EnsureQueue(queueName, exchangeName).Model;

        byte[] body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(obj));

        channel.BasicPublish(exchange: exchangeName, routingKey: queueName, basicProperties: null, body: body);
    }


    public static EventingBasicConsumer CreateBasicConsumer()
    {
        var factory = new ConnectionFactory()
        {
            HostName = RabbitMQConstant.RabbitMQHost
        };

        var connection = factory.CreateConnection();

        var channel = connection.CreateModel();

        return new EventingBasicConsumer(channel);
    }

    public static EventingBasicConsumer EnsureExchange(this EventingBasicConsumer eventingBasicConsumer, string exchangeName, string exchangeType = RabbitMQConstant.DefaultExchangeType)
    {
        eventingBasicConsumer.Model.ExchangeDeclare(exchange: exchangeName, type: exchangeType, durable: false, autoDelete: false);

        return eventingBasicConsumer;
    }

    public static EventingBasicConsumer EnsureQueue(this EventingBasicConsumer eventingBasicConsumer, string queueName, string exchangeName)
    {
        eventingBasicConsumer.Model.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

        eventingBasicConsumer.Model.QueueBind(queue: queueName, exchange: exchangeName, routingKey: queueName);

        return eventingBasicConsumer;
    }
}
