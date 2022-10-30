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

            _eventStore.Raise(_parkingSpaces.RegisterNumber, 
            "Parking",
            DateTime.Now,
            _parkingSpaces.ParkingSpace,
            _parkingSpaces.ParkingFloor,
            _parkingSpaces.Email,
            _parkingSpaces.PhoneNumber);

        }

        public void delete(ParkingSpaces _parkingSpaces, IEventStore _eventStore)
        {
            Database.Remove(_parkingSpaces.RegisterNumber);

            _eventStore.Raise(_parkingSpaces.RegisterNumber,
            "Delete",
            DateTime.Now,
            _parkingSpaces.ParkingSpace,
            _parkingSpaces.ParkingFloor,
            _parkingSpaces.Email,
            _parkingSpaces.PhoneNumber);
        }

        public Boolean checkParking(ParkingSpaces _parkingSpaces)
        {
            ParkingSpaces result;

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
