using CarTypeService.Models;
using CarTypeService.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MotorApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MotorApiController : ControllerBase
    {
        private readonly string licensePlate = "CR30188";

        private readonly ILogger<MotorApiController> _logger;
        MotorApiService motorApiService;

        public MotorApiController(ILogger<MotorApiController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetMotorApi")]
        public IActionResult Get()
        {
            CarDescription description = new CarDescription();

            //motorApiService = new MotorApiService("www.test.com", );
            description = motorApiService.GetDescriptionAsync(licensePlate).Result as CarDescription;

            //string json = "TEST";

            return new OkObjectResult(JsonConvert.SerializeObject(description.Make + description.Model + description.Variant));
            //return (IEnumerable<MotorApiService>)okObjectResult;
        }
    }
}