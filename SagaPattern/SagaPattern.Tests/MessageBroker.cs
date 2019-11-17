using System;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace SagaPattern.Tests
{
    internal class MessageBroker
    {
        internal static void Initialize()
        {
            string userName = "guest";
            string password = "guest";
            string hostName = "localhost";

            var factory = new ConnectionFactory() {
                UserName = userName,
                Password = password,
                HostName = hostName
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("CustomerServiceRequestExchange", ExchangeType.Direct);
                channel.QueueDeclare("CustomerServiceRequest", true, false, false, null);
                channel.QueueBind("CustomerServiceRequest", "CustomerServiceRequestExchange", "CustomerServiceRequest_direct");

                channel.ExchangeDeclare("CustomerServiceReplyExchange", ExchangeType.Direct);
                channel.QueueDeclare("CustomerServiceReply", true, false, false, null);
                channel.QueueBind("CustomerServiceReply", "CustomerServiceReplyExchange", "CustomerServiceReply_direct");

            }
        }
    }
}