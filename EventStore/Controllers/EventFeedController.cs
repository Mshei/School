using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace EventStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventFeedController : ControllerBase
    {
        private readonly IEventStore eventStore;
        public EventFeedController(IEventStore eventStore) =>
        this.eventStore = eventStore;
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            eventStore.GetEvents().Values.ToList();

            string json;
            json = JsonConvert.SerializeObject(eventStore.GetEvents().Values.ToList());
            
            if(json != null)
            {
                return new OkObjectResult(json);

            }
            return Ok(json);
        }
    }
}
