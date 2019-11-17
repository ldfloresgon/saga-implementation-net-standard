using Messages.Events;
using Messages.Events.CustomerService;
using Newtonsoft.Json;
using SagaPattern;

namespace OrderService
{
    public class OrderSagaParticipant : SagaParticipant<CreateOrderSagaData>,
        ISagaParticipant<CustomerCreditReservedEvent, CustomerCreditExceededEvent>
    {
        private readonly IServiceBusSubscriber _serviceBusSubscriber;
        private readonly string _queue;
        private readonly IOrderRepository _orderRepository;

        public OrderSagaParticipant(IServiceBusSubscriber serviceBusSubscriber,
            IOrderRepository orderRepository,
            string queue)
        {
            _serviceBusSubscriber = serviceBusSubscriber;
            _queue = queue;
            _orderRepository = orderRepository;
        }


        public override void OnSubscribe()
        {
            _serviceBusSubscriber.To(_queue, (messageType, payload, messageId) =>
            {
                if (messageType.ToLowerInvariant() == nameof(CustomerCreditReservedEvent).ToLowerInvariant())
                {
                    CustomerCreditReservedEvent customerCreditReservedEvent = JsonConvert.DeserializeObject<CustomerCreditReservedEvent>(payload);
                    Success(customerCreditReservedEvent);
                }
                else if (messageType.ToLowerInvariant() == nameof(CustomerCreditExceededEvent).ToLowerInvariant())
                {
                    CustomerCreditExceededEvent customerCreditReservedEvent = JsonConvert.DeserializeObject<CustomerCreditExceededEvent>(payload);
                    Failure(customerCreditReservedEvent);
                }
            });
        }

        public void Success(CustomerCreditReservedEvent vent)
        {
            _orderRepository.Success();
        }

        public void Failure(CustomerCreditExceededEvent @event)
        {
            _orderRepository.Compensate(@event.Reason);
        }
    }
}
