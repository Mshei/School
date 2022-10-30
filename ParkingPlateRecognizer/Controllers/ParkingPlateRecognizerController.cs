using Microsoft.AspNetCore.Mvc;
using PlateRecognizer;

namespace ParkingPlateRecognizer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParkingPlateRecognizerController : Controller
    {
        IParkingPlateRecognizerService parkingLotServices;
        public ParkingPlateRecognizerController(IParkingPlateRecognizerService iParkingPlateRecognizerService)
        {
            parkingLotServices = iParkingPlateRecognizerService;
        }
        [HttpGet(Name = "GetPicturePlate")]
        public async Task<IActionResult> Get(String email,
            String phoneNumber)
        {
            string token = "a07fff9f86252e925221aeb5eb0510610eb5e59d";

            string fileName = @"D:\PlateRecognizer\PlateRecognizer\Biler\bil01.png";

            var fileData = System.IO.File.ReadAllBytes(fileName);

            string postUrl = "http://api.platerecognizer.com/v1/plate-reader/";
            string regions = "dk";
            PlateReaderResult plateReaderResult = PlateReader.Read(postUrl, null, fileData, regions, token, false);

            if (plateReaderResult.Results.Count == 0)
            {
                return new OkObjectResult("Der blev ikke fundet nogen nummerplader");
            }

            Result result = plateReaderResult.Results[0];

            Random rnd = new Random();

            int parkingFloor = rnd.Next(1, 10);
            String parkingSpace = "P" + rnd.Next(1, 10);

            EventStore.Event _event = new EventStore.Event(result.Plate, "Parking",
            DateTime.Now,
            parkingSpace,
            parkingFloor,
            email,
            phoneNumber);
            Boolean checkParking = false;
            checkParking = await parkingLotServices.checkEventAsync(result.Plate, _event);

            //return new OkObjectResult($"Nummerplanen er  {result.Plate} med {result.Score * 100: 0}% sandsynlighed");
            return new OkObjectResult($"{result.Plate}");
        }
    }
}