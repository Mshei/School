namespace EventStore
{
    public record Event(
    string licensePlate,
    DateTimeOffset OccuredAt,
    object Content,
    string eventOccured);

}
