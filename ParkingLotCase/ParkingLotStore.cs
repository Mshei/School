using EventStore;
using static ParkingLotCase.ParkingLotStore;

namespace ParkingLotCase
{
    public class ParkingLotStore 
    {
        private static readonly Dictionary<string, ParkingSpaces>
            Database = new Dictionary<string, ParkingSpaces>();
        public ParkingSpaces Get(string _registerNumber) =>
        Database.ContainsKey(_registerNumber)
        ? Database[_registerNumber]
        : new ParkingSpaces(_registerNumber);
        public void Save(ParkingSpaces _parkingSpaces, IEventStore _eventStore)
        {
            _parkingSpaces.IsParked = true;
            Database[_parkingSpaces.RegisterNumber] = _parkingSpaces;

            _eventStore.Raise(
            _parkingSpaces.RegisterNumber,
            new { _parkingSpaces }, "Saved");

        }

        public void delete(ParkingSpaces _parkingSpaces, IEventStore _eventStore)
        {
            Database.Remove(_parkingSpaces.RegisterNumber);

            _eventStore.Raise(
            _parkingSpaces.RegisterNumber,
            new { _parkingSpaces }, "Deleted");
        }

        public Boolean checkParking(ParkingSpaces _parkingSpaces)
        {
            ParkingSpaces result, tmp;

            if (Database.TryGetValue(_parkingSpaces.RegisterNumber, out result))
            {
                if (result.IsParked)
                {
                    return true;
                }
            }
            
            return false;
        }
    }

    
}
