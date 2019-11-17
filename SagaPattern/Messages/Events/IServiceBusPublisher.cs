using System.Threading.Tasks;

namespace Messages.Events
{
    public interface IServiceBusPublisher
    {
        Task Publish(string exchange, IMessage @message);
    }
}
