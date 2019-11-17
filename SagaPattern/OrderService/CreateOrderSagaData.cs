using SagaPattern;
using System;

namespace OrderService
{
    public class CreateOrderSagaData : SagaData
    {
        public string OrderId { get; set; }
        public int TotalAmount { get; set; }
        public string CustomerId { get; set; }

        public CreateOrderSagaData(string orderId, int totalAmount, string customerId)
        {
            this.OrderId = orderId;
            this.TotalAmount = totalAmount;
            this.CustomerId = customerId;
        }
    }
}
