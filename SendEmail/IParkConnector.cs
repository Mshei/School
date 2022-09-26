namespace SendEmail
{
    using System.Threading.Tasks;

    public interface IOrderConnector
    {
        Task<ParkInfo> GetNextOrder();
        Task RemoveOrder(ParkInfo order);
    }
}
