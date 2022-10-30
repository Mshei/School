using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace EventStore
{
    public class EventStore : IEventStore
    {
        Dictionary<string, Event> dataBase = new Dictionary<string, Event>();

        public void Raise(string _licensePlate, string _typeEvent, DateTime _occuredAt, string _parkingSpace, int _parkingFloor, string _email, string _phoneNumber)
        {
            dataBase.Add(_licensePlate, new Event(_licensePlate, "Parking",
            DateTime.Now,
            _parkingSpace,
            _parkingFloor,
            _email,
            _phoneNumber));
        }
        public Boolean GetEvents(string licenseNumber, Event content)
        {
            KeyValuePair<string, Event> dataBaseSearch = new KeyValuePair<string, Event>(licenseNumber, content);

            // first sample => test just the key if it's present in the dictionnary
            if (dataBase.ContainsKey(dataBaseSearch.Key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        Boolean IEventStore.remove(string licenseNumber, Event content)
        { 
            Boolean removed = false;
            KeyValuePair<string, Event> dataBaseSearch = new KeyValuePair<string, Event>(licenseNumber, content);

            if (dataBase.ContainsKey(dataBaseSearch.Key))
            {
                dataBase.Remove(dataBaseSearch.Key);

                removed = true;
            }

            return removed;
        }

        Boolean IEventStore.update(string licenseNumber, Event content, Event newContent)
        { 
            Boolean updated = false;

            KeyValuePair<string, Event> dataBaseSearch = new KeyValuePair<string, Event>(licenseNumber, content);
            KeyValuePair<string, Event> newDataBase = new KeyValuePair<string, Event>(licenseNumber, newContent);

            if (dataBase.ContainsKey(dataBaseSearch.Key))
            {
                dataBase.Remove(dataBaseSearch.Key);
                dataBase.Add(licenseNumber, newContent);

                updated = true;
            }

            return updated;
        }

        public Dictionary<string, Event> getDictionary()
        {
            return dataBase;
        }
    }
}
