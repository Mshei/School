using Microsoft.Extensions.Logging;

namespace EventStore
{
    public interface IEventStore
    {
        public Dictionary<string, Event> GetEvents();
        void Raise(string eventName, object content, string eventOccured);
       
    }
    
}
