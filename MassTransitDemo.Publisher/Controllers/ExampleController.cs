using MassTransit;
using MassTransitDemo.Shared.Events.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MassTransitDemo.Publisher.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExampleController : ControllerBase
    {
        private readonly ILogger<ExampleController> _logger;
        private readonly IBus _busService;

        public ExampleController(ILogger<ExampleController> logger,
            IBus busService)
        {
            _logger = logger;
            _busService = busService;
        }

        [HttpPost]
        public async Task<ActionResult> Post(CustomerProductEvent @event)
        {
            var uri = new Uri("rabbitmq://localhost/customerProductEvent");
            
            var endpoint = await _busService.GetSendEndpoint(uri);

            @event.CreatedDate = DateTime.Now;
            await endpoint.Send(@event);

            return Ok();
        }
    }
}
