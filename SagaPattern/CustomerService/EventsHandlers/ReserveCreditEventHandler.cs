using System.Threading.Tasks;
using Messages.Commands;
using Messages.Events;
using Messages.Events.CustomerService;

namespace CustomerService.CommandHandlers
{
    public class ReserveCreditEventHandler : IEventHandler
    {
        readonly IServiceBusPublisher _serviceBusPublisher;
        readonly IServiceBusSubscriber _serviceBusSubscriber;
        public ReserveCreditEventHandler(IServiceBusPublisher serviceBusPublisher,
            IServiceBusSubscriber serviceBusSubscriber)
        {
            _serviceBusPublisher = serviceBusPublisher;
            _serviceBusSubscriber = serviceBusSubscriber;
        }


        public void Handle()
        {
            _serviceBusSubscriber.To("CustomerServiceRequest", async (messageType, payload, messageId) =>
            {
                if (messageType.ToLowerInvariant() == nameof(ReserveCreditCommand).ToLowerInvariant())
                {
                    await _serviceBusPublisher.Publish("CustomerServiceReply", new CustomerCreditExceededEvent("Exceeded customer credit limit"));
                }

            });
        }
    }
}
