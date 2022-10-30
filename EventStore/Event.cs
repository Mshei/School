namespace EventStore
{
    public class Event
    {
        public Event(string _licensePlate, string _typeEvent, DateTime _occuredAt, string _parkingSpace, int _parkingFloor, string _email, string _phoneNumber)
        {
            this.licensePlate = _licensePlate;
            this.typeEvent = _typeEvent;
            this.OccuredAt = _occuredAt;
            this.ParkingSpace = _parkingSpace;
            this.ParkingFloor = _parkingFloor;
            this.Email = _email;
            this.PhoneNumber = _phoneNumber;

        }
        public string licensePlate { get; set; }
        public string typeEvent { get; set; }
        public DateTime OccuredAt { get; set; }
        public string ParkingSpace { get; set; }
        public int ParkingFloor { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

}
