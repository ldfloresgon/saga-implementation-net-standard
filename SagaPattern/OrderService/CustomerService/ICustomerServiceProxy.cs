using Messages.Commands;

namespace OrderService.CustomerService
{
    public interface ICustomerServiceProxy
    {
        void ReserCreditCommand(ReserveCreditCommand reserveCreditCommand);
    }
}