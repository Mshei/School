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
        public void Save(ParkingSpaces _parkingSpaces)
        {
            _parkingSpaces.IsParked = true;
            Database[_parkingSpaces.RegisterNumber] = _parkingSpaces;

        }

        public void delete(string _registerNumber)
        {
            Database.Remove(_registerNumber);
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
