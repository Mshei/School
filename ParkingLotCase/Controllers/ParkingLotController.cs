using EventStore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SendParkingEmail.Configurations;
using SendParkingEmail.Controllers;
using SendParkingEmail.Model;
using SendParkingEmail.Services;

namespace ParkingLotCase.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParkingLotController : Controller
    {
        private readonly ILogger<ParkingLotController> _logger;
        IParkingLotServices parkingLotServices;
        private readonly IEventStore eventStore;
        public ParkingLotController(ILogger<ParkingLotController> logger, IParkingLotServices iparkingLotServices, IEventStore eventStore)
        {
            _logger = logger;
            parkingLotServices = iparkingLotServices;
            this.eventStore = eventStore;
        }
        [HttpGet(Name = "SetParkingLot")]
        public async Task<IActionResult> Get(String _phoneNumber, String _email, String _regNum)
        {
            Random rnd = new Random();

            ParkingSpaces registerParkingSpace = new ParkingSpaces(_regNum);
            registerParkingSpace.PhoneNumber = _phoneNumber;
            registerParkingSpace.Email = _email;
            registerParkingSpace.DateTime = DateTime.Now;
            registerParkingSpace.ParkingFloor = rnd.Next(1, 10);
            registerParkingSpace.ParkingSpace = "P" + rnd.Next(1, 10);

            registerParkingSpace = await parkingLotServices.GetDescriptionAsync(registerParkingSpace, _regNum);

            ParkingLotStore parkingLotStore = new ParkingLotStore();
            parkingLotStore.Save(registerParkingSpace, eventStore);

            registerParkingSpace.emailSent = await parkingLotServices.SendEmailAsync(_regNum, _email);

            ParkingLotReturnJson parkingLotReturnJson = new ParkingLotReturnJson();

            return parkingLotReturnJson.result(registerParkingSpace);

        }
    }
}
