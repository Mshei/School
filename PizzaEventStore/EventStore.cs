using EventStore;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace PizzaEventStore
{
    public class EventStore : IEventStore
    {
        List<KeyValuePair<int, newEvent>> dataBase = new List<KeyValuePair<int, newEvent>>();

        public String Raise(int tableNumber, newEvent content, string pizzaNumber)
        {
            dataBase.Add(new KeyValuePair<int, newEvent>(tableNumber, new newEvent(tableNumber,
            DateTimeOffset.UtcNow,
            content, pizzaNumber)));

            return "New event have been created";

        }
        public String GetEvents(int tableNumber)
        {
            String orders;

            var result = from s in dataBase
                         where s.Key == tableNumber
                         select s;

            orders = "";

            foreach (var Order in result)
            {
                orders += Order.Value + "\n";
            }

            return String.Format("The table has ordrered: {0}", orders);
        }
        public Boolean remove(int tableNumber, newEvent content)
        {
            var result = from s in dataBase
                         where s.Key == tableNumber
                         select s;

            Boolean deleted = false;

            foreach (var Order in result)
            {
                dataBase.Remove(new KeyValuePair<int, newEvent>(tableNumber, content));

                deleted = true;
            }

            if(deleted != false)
            { 
                return true;;

            }
            return false; ;

        }
        public Boolean update(int tableNumber, newEvent content, string pizzaNumber)
        {
            int removalStatus = dataBase.RemoveAll(x => x.Key == tableNumber);

            if (removalStatus == 1)
            {
                dataBase.Add(new KeyValuePair<int, newEvent>(tableNumber, new newEvent(tableNumber,
                DateTimeOffset.UtcNow,
                content, pizzaNumber)));
                return true;

            }

            return false;
        }

    }
}
