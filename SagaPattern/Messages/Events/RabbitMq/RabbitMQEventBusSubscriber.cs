using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Messages.Events.RabbitMq
{
    public class RabbitMQEventBusSubscriber : IServiceBusSubscriber
    {
        private readonly IModel _channel;
        public RabbitMQEventBusSubscriber(IConfiguration configuration)
        {
            IConnection conn = new ConnectionFactory()
            {
                UserName = configuration["RabbitMQ:UserName"],
                Password = configuration["RabbitMQ:Password"],
                VirtualHost = configuration["RabbitMQ:VirtualHost"],
                HostName = configuration["RabbitMQ:Host"]
            }.CreateConnection();

            _channel = conn.CreateModel();
        }

        public void To(string queueName, Action<string, string, ulong> callback)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (sender, deliverArgs) =>
            {
                string payload = Encoding.UTF8.GetString(deliverArgs.Body);
                string messageType = deliverArgs.BasicProperties.Type;

                callback(messageType, payload, deliverArgs.DeliveryTag);
            };

            bool autoAck = true;

            _channel.BasicConsume(queueName, autoAck, consumer);
        }
    }
}
