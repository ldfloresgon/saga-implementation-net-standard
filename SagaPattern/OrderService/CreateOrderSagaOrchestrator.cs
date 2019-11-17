using Messages.Commands;
using Messages.Events;
using OrderService.CustomerService;
using SagaPattern;

namespace OrderService
{
    public class CreateOrderSagaOrchestrator : SagaOrchestrator<CreateOrderSagaData>
    {
        readonly ICustomerServiceProxy _customerServiceProxy;
        readonly IOrderRepository _orderRepository;

        public CreateOrderSagaOrchestrator(ICustomerServiceProxy customerServiceProxy,
            IServiceBusSubscriber serviceBusSubscriber,
            IOrderRepository orderRepository)
        {
            _customerServiceProxy = customerServiceProxy;
            _orderRepository = orderRepository;

            base.AddSagaParticipant(new OrderSagaParticipant(
                serviceBusSubscriber, 
                _orderRepository, 
                "CustomerServiceReply")
            );
        }

        public override void Execute()
        {
            CreateOrderSagaData sagaData = this.StateMachine.Data;

            _customerServiceProxy.ReserCreditCommand(new ReserveCreditCommand(
                sagaData.OrderId,
                sagaData.TotalAmount,
                sagaData.CustomerId,
                sagaData.CorrelationId)
            );
        }
    }
}
