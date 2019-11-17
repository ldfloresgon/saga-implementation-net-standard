using Commands;
using System.Threading.Tasks;

namespace Events
{
    public interface IServiceBusPublisher
    {
        Task Publish(string exchange, ICommand @command);
        Task Publish(string exchange, IEvent @event);
    }
}
