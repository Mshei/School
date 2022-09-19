using System.ComponentModel;

namespace ParkingCaseOne.Model
{
    public class ParkingLotDetails
    {
        [DisplayName("Parking space number")]
        public string parkingSpaceNumber   
            { get; set; }
        [DisplayName("Floor")]
        public int floor
            { get; set; }
        public Boolean occupied
            { get; set; }
        public string registerNumber
            { get; set; }

    }
}
