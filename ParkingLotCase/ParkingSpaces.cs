namespace ParkingLotCase
{
    public class ParkingSpaces
    {
        public ParkingSpaces(string _registerNumber)
        {
            this.RegisterNumber = _registerNumber;
        }

        public DateTime DateTime { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ParkingSpace { get; set; }
        public string RegisterNumber { get; set; }
        public int ParkingFloor { get; set; }
        public Boolean IsParked { get; set; }

    }
}
