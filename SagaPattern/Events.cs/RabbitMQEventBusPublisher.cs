using Commands;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    public class RabbitMQEventBusPublisher : IServiceBusPublisher
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;


        public RabbitMQEventBusPublisher(IConfiguration configuration)
        {
            _connection = new ConnectionFactory()
            {
                UserName = configuration["RabbitMQ:UserName"],
                Password = configuration["RabbitMQ:Password"],
                VirtualHost = configuration["RabbitMQ:VirtualHost"],
                HostName = configuration["RabbitMQ:Host"]
            }.CreateConnection();

            _channel = _connection.CreateModel();
        }

        public async Task Publish(string exchange, ICommand @command)
        {
            await Task.Run(() =>
            {
                try
                {
                    _channel.ExchangeDeclare(exchange, ExchangeType.Direct, true);

                    string payload = JsonConvert.SerializeObject(@command);

                    byte[] messageBodyBytes = Encoding.UTF8.GetBytes(payload);

                    IBasicProperties props = _channel.CreateBasicProperties();

                    props.Type = $"{@command.GetType().Name}";

                    _channel.BasicPublish(exchange, "", props, messageBodyBytes);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }
    }
}
