namespace ParkingLotCase
{
    public class ParkingCarDescription
    {
            public string Make { get; set; }
            public string Model { get; set; }
            public string Variant { get; set; }
            public static ParkingCarDescription NONE = new();
    }
}
