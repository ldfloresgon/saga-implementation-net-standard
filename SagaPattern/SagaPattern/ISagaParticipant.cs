using Messages.Events;

namespace SagaPattern
{
    public interface ISagaParticipant<TSuccessEvent, TFailureEvent>
        where TSuccessEvent : IEvent
        where TFailureEvent : IEvent
    {
        void Success(TSuccessEvent @vent);
        void Failure(TFailureEvent @event);
    }
}
