using System;

namespace Events
{
    public interface IServiceBusSubscriber
    {
        void To(string queueName, Action<string, string, ulong> callback);
        //void Nack(ulong deliveryTag);
        //void Ack(ulong deliveryTag);        
    }
}
