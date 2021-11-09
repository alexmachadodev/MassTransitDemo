using MassTransit;
using MassTransitDemo.Shared.Events.Events;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading.Tasks;

namespace MassTransitDemo.Consumer
{
    public class CustomerProductConsumer : IConsumer<CustomerProductEvent>
    {
        readonly ILogger<CustomerProductConsumer> _logger;

        public CustomerProductConsumer(ILogger<CustomerProductConsumer> logger) => _logger = logger;

        public Task Consume(ConsumeContext<CustomerProductEvent> context)
        {
            _logger.LogInformation("Received event: {event}", JsonSerializer.Serialize(context.Message));

            return Task.CompletedTask;
        }
    }
}
