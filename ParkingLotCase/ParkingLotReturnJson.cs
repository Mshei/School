using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ParkingLotCase
{
    public class ParkingLotReturnJson 
    {
        public DateTime DateTime { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ParkingSpace { get; set; }
        public string RegisterNumber { get; set; }
        public int ParkingFloor { get; set; }
        public Boolean IsParked { get; set; }
        public Boolean emailSent { get; set; }
        /*
         * I have created two results methods, are one of them better then the other?
         * */
        public IActionResult result2(ParkingSpaces _registerParkingSpace)
        {
            ParkingLotStore parkingLotStore = new ParkingLotStore();
            string json;
            const string V = "not parked";

            if (parkingLotStore.checkParking(_registerParkingSpace))
            {
                json = JsonConvert.SerializeObject(string.Format("Car {0} is parked at floor {1} at parking space {2} ", _registerParkingSpace.RegisterNumber, _registerParkingSpace.ParkingFloor, _registerParkingSpace.ParkingSpace));

                return new OkObjectResult(json);

            }
            else
            { 
                json = JsonConvert.SerializeObject(string.Format("Car {0} is not parked ", _registerParkingSpace.RegisterNumber));
                return new OkObjectResult(json);
            }
        
        }
        public IActionResult result(ParkingSpaces _registerParkingSpace)
        {
            ParkingLotStore parkingLotStore = new ParkingLotStore();

            if (parkingLotStore.checkParking(_registerParkingSpace))
            { 
                string json = JsonConvert.SerializeObject(new
                {
                    results = new List<ParkingLotReturnJson>()
                    {
                    new ParkingLotReturnJson { ParkingFloor = _registerParkingSpace.ParkingFloor, 
                                           Email = _registerParkingSpace.Email, 
                                           PhoneNumber = _registerParkingSpace.PhoneNumber, 
                                           DateTime = _registerParkingSpace.DateTime, 
                                           IsParked = _registerParkingSpace.IsParked, 
                                           ParkingSpace = _registerParkingSpace.ParkingSpace, 
                                           RegisterNumber = _registerParkingSpace.RegisterNumber,
                                           emailSent = _registerParkingSpace.emailSent},
                    }
                });
                return new OkObjectResult(json);

            }

            return (this.EmptyActionResult());
        }

        public EmptyResult EmptyActionResult()
        {
            return new EmptyResult();
        }
    }
}
