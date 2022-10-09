namespace PizzaEventStore
{
    public record newEvent(
    int tableNumber,
    DateTimeOffset OccuredAt,
    object Content,
    string pizzaNumber);
}
