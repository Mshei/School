namespace ParkingCaseOne.Model
{
    public class ParkingLotErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
