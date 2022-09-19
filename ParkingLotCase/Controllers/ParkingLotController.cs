using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ParkingLotCase.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParkingLotController : Controller
    {
        private readonly ILogger<ParkingLotController> _logger;

        public ParkingLotController(ILogger<ParkingLotController> logger)
        {
            _logger = logger;
        }
        [HttpGet(Name = "GetParkingLot")]
        public IActionResult Get()
        {
            ParkingSpaces registerParkingSpace = new ParkingSpaces("DC5832");
            registerParkingSpace.PhoneNumber = "+45 12345678";
            registerParkingSpace.Email = "my@email.org";
            registerParkingSpace.DateTime = DateTime.Now;
            registerParkingSpace.ParkingFloor = 2;
            registerParkingSpace.ParkingSpace = "P1";

            ParkingLotStore parkingLotStore = new ParkingLotStore();
            parkingLotStore.Save(registerParkingSpace);

            string json;

            const string V = "not parked";


            if (parkingLotStore.checkParking(registerParkingSpace))
            {
                json = JsonConvert.SerializeObject(string.Format("Car {0} is parked at floor {1} at parking space {2} ", registerParkingSpace.RegisterNumber, registerParkingSpace.ParkingFloor, registerParkingSpace.ParkingSpace));

                return new OkObjectResult(json);

            }
            else
            { 
                json = JsonConvert.SerializeObject(string.Format("Car {0} is not parked ", registerParkingSpace.RegisterNumber));
                return Ok(json);
            }


        }
    }
}
