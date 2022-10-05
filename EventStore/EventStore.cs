namespace EventStore
{
    public class EventStore : IEventStore
    {
        Dictionary<string, Event>
        database = new Dictionary<string, Event>();

        public void Raise(string eventName, object content, string eventOccured)
        {
            database.Add(eventName, new Event(eventName,
            DateTimeOffset.UtcNow,
            content, eventOccured));
        }
        public Dictionary<string, Event> GetEvents()
        {
            return database;
        }
    }
}
