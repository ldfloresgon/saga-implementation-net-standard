using CustomerService.CommandHandlers;
using Messages.Events.RabbitMq;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using OrderService;
using OrderService.CustomerService;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SagaPattern.Tests
{
    public class CreateOrderSagaOrchestratorShould
    {
        [Test]
        public async Task Initiaze_a_saga()
        {
            Mock<IOrderRepository> orderRepository = new Mock<IOrderRepository>();

            var config = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
               // .AddEnvironmentVariables()
               .Build();

            MessageBroker.Initialize();


            CreateOrderSagaOrchestrator createOrderSagaOrchestrator =
                new CreateOrderSagaOrchestrator(
                    new CustomerServiceProxy(new RabbitMQEventBusPublisher(config)),
                    new RabbitMQEventBusSubscriber(config),
                    orderRepository.Object);

            string orderId = Guid.NewGuid().ToString();
            string customerId = Guid.NewGuid().ToString();
            int totalAmount = 1000;

            CreateOrderSagaData sagaData = new CreateOrderSagaData(orderId, totalAmount, customerId);

            createOrderSagaOrchestrator.Initialize(sagaData);
            createOrderSagaOrchestrator.Execute();


            ReserveCreditEventHandler reserveCreditCommandHandler = new ReserveCreditEventHandler(new RabbitMQEventBusPublisher(config),
                new RabbitMQEventBusSubscriber(config));

            reserveCreditCommandHandler.Handle();

            Thread.Sleep(2000);

            orderRepository.Verify(_ => _.Compensate("Exceeded customer credit limit"), Times.Once);
        }
    }
}