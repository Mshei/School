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
        [HttpGet(Name = "GetEvents")]
        public async Task<IActionResult> Get(string _licensePlate, Event _event)
        {
            string json = "";
            /*
            foreach (var dictionary in eventStore.getDictionary())
            {
                json += ("dictionary key is {0} and value is {1}", dictionary.Key, dictionary.Value);
            }*/

            json = JsonConvert.SerializeObject(eventStore.GetEvents(_licensePlate, _event));
            
            if(json != null)
            {
                return new OkObjectResult(json);

            }
            return Ok(json);
        }
    }
}
