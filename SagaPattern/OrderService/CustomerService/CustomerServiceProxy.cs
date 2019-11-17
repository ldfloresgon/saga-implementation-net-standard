
using Messages.Commands;
using Messages.Events;

namespace OrderService.CustomerService
{
    public class CustomerServiceProxy : ICustomerServiceProxy
    {
        readonly IServiceBusPublisher _serviceBusPublisher;

        public CustomerServiceProxy(IServiceBusPublisher serviceBusPublisher)
        {
            _serviceBusPublisher = serviceBusPublisher;
        }

        public void ReserCreditCommand(ReserveCreditCommand reserveCreditCommand)
        {
            _serviceBusPublisher.Publish("CustomerServiceRequest", reserveCreditCommand);
        }
    }
}
