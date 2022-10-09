using EventStore;

namespace PizzaEventStore
{
    public interface IEventStore
    {
        public String GetEvents(int tableNumber);
        String Raise(int tableNumber, newEvent content, string pizzaNumber);

        Boolean remove(int tableNumber, newEvent content);

        Boolean update(int tableNumber, newEvent content, string pizzaNumber);

    }
}
