using System;

namespace Messages.Commands
{
    public class ReserveCreditCommand : ICommand
    {
        public Guid CorrelationId { get; set; }
        public string OrderId { get; set; }
        public int TotalAmount { get; set; }
        public string CustomerId { get; set; }


        public ReserveCreditCommand(string orderId, int totalAmount, string customerId, Guid correlationId)
        {
            this.OrderId = orderId;
            this.TotalAmount = totalAmount;
            this.CustomerId = customerId;
            this.CorrelationId = correlationId;
        }
    }
}
