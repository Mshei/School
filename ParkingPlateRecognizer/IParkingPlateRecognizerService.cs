namespace ParkingPlateRecognizer
{
    public interface IParkingPlateRecognizerService
    {
        Task<Boolean> checkEventAsync(string _licensePlate, EventStore.Event _event);
    }
}
