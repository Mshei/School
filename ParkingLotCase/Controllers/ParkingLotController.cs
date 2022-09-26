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
            ParkingSpaces registerParkingSpace = new ParkingSpaces("DC58321");
            registerParkingSpace.PhoneNumber = "+45 12345678";
            registerParkingSpace.Email = "my@email.org";
            registerParkingSpace.DateTime = DateTime.Now;
            registerParkingSpace.ParkingFloor = 2;
            registerParkingSpace.ParkingSpace = "P1";

            ParkingLotStore parkingLotStore = new ParkingLotStore();
            parkingLotStore.Save(registerParkingSpace);

            ParkingLotReturnJson parkingLotReturnJson = new ParkingLotReturnJson();

            return parkingLotReturnJson.result(registerParkingSpace);

        }
    }
}
