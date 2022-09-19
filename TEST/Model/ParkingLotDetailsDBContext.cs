using Microsoft.EntityFrameworkCore;

namespace ParkingCaseOne.Model
{
    public class ParkingLotDetailsDBContext : DbContext
    {
        /*public ParkingLotDetailsDBContext (DbContextOptions <ParkingLotDetailsDBContext> options) : base (options)
        { 
            
        */

        public DbSet <Model.ParkingLotDetails> CarDetails { get; set; }
    }
}
