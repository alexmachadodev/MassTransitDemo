using System;

namespace MassTransitDemo.Shared.Events.Events
{
    public class CustomerProductEvent
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
