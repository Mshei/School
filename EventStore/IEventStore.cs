using Microsoft.Extensions.Logging;

namespace EventStore
{
    public interface IEventStore
    {
        void Raise(string _licensePlate, string _typeEvent, DateTime _occuredAt, string _parkingSpace, int _parkingFloor, string _email, string _phoneNumber);
        public Boolean GetEvents(string licenseNumber, Event content);
        Dictionary<string, Event> getDictionary();

        Boolean remove(string licenseNumber, Event content);
        Boolean update(string licenseNumber, Event Content, Event newContent);

    }
    
}
