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
        //private readonly string licensePlate = "CR30188";

        private readonly ILogger<MotorApiController> _logger;
        IMotorApiService motorApiService;

        public MotorApiController(ILogger<MotorApiController> logger, IMotorApiService imotorApiService)
        {
            _logger = logger;
            motorApiService = imotorApiService;
        }

        [HttpGet(Name = "GetMotorApi")]
        public async Task<IActionResult> Get(string licensePlate)
        {
            CarDescription description = new CarDescription();

            description = await motorApiService.GetDescriptionAsync(licensePlate);

            return new OkObjectResult(JsonConvert.SerializeObject(description.Make + description.Model + description.Variant));
        }
    }
}