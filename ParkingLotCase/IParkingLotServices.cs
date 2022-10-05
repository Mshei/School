namespace ParkingLotCase
{
    public interface IParkingLotServices
    {
        Task<ParkingSpaces> GetDescriptionAsync(ParkingSpaces parkingSpaces, string licensePlate);
        Task<bool> SendEmailAsync(String _licensePlate, String _email);
    }
}
