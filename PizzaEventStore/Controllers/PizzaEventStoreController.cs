using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PizzaEventStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzaEventStoreController : ControllerBase
    {
        private readonly ILogger<PizzaEventStoreController> _logger;
        private IEventStore eventStore;

        public PizzaEventStoreController(ILogger<PizzaEventStoreController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetEvents")]
        public async Task<IActionResult> GetEvents(int tableNumber)
        {
            eventStore = new EventStore();
            return new OkObjectResult(JsonConvert.SerializeObject(eventStore.GetEvents(tableNumber)));
        }

        [HttpPost(Name = "PostEvent")]
        public async Task<IActionResult> RaiseEvent(int _tableNumber, PizzaEventStore.newEvent _event, string _pizzaNumber)
        {
            eventStore = new EventStore();
            return new OkObjectResult(JsonConvert.SerializeObject(eventStore.Raise(_tableNumber, _event, _pizzaNumber)));
        }

        [HttpPut(Name = "UpdateEvent")]
        public async Task<IActionResult> updateEvent(int _tableNumber, PizzaEventStore.newEvent _event, string _pizzaNumber)
        {
            eventStore = new EventStore();
            return new OkObjectResult(JsonConvert.SerializeObject(eventStore.update(_tableNumber, _event, _pizzaNumber)));
        }

        [HttpDelete(Name = "DeleteEvent")]
        public async Task<IActionResult> deleteEvent(int _tableNumber, PizzaEventStore.newEvent _event)
        {
            eventStore = new EventStore();
            return new OkObjectResult(JsonConvert.SerializeObject(eventStore.remove(_tableNumber, _event)));
        }
    }
}