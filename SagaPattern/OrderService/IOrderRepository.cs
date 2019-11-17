namespace OrderService
{
    public interface IOrderRepository
    {
        void Success();
        void Compensate(string reason);
    }
}
