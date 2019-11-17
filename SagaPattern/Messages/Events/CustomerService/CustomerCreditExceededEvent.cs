namespace Messages.Events.CustomerService
{
    public class CustomerCreditExceededEvent : IEvent
    {
        public string Reason { get; set; }

        public CustomerCreditExceededEvent(string reason)
        {
            this.Reason = reason;
        }
    }
}
