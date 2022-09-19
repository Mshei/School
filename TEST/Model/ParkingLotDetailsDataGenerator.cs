using Microsoft.EntityFrameworkCore;

namespace ParkingCaseOne.Model
{
    public class ParkingLotDetailsDataGenerator
    {
        public void Initialize(IServiceProvider serviceProvider)
        {
            /*using (var context = new ParkingLotDetailsDBContext(
                serviceProvider.GetRequiredService<DbContextOptions<ParkingLotDetailsDBContext>>()))
            {
                // Look for any board games.
                if (context.CarDetails.Any())
                {
                    return;   // Data was already seeded
                }

                context.CarDetails.AddRange(
                    new ParkingLotDetails
                    {
                        parkingSpaceNumber = "P1",
                        floor = 1,
                        occupied = false,
                        registerNumber = ""
                    },
                    new ParkingLotDetails
                    {
                        parkingSpaceNumber = "P2",
                        floor = 1,
                        occupied = true,
                        registerNumber = "CV30155"
                    },
                    new ParkingLotDetails
                    {
                        parkingSpaceNumber = "P3",
                        floor = 1,
                        occupied = true,
                        registerNumber = "DA54655"
                    },
                    new ParkingLotDetails
                    {
                        parkingSpaceNumber = "P1",
                        floor = 2,
                        occupied = true,
                        registerNumber = "AX13685"
                    },
                    new ParkingLotDetails
                    {
                        parkingSpaceNumber = "P2",
                        floor = 2,
                        occupied = false,
                        registerNumber = ""
                    },
                    new ParkingLotDetails
                    {
                        parkingSpaceNumber = "P3",
                        floor = 2,
                        occupied = false,
                        registerNumber = ""
                    },
                    new ParkingLotDetails
                    {
                        parkingSpaceNumber = "P1",
                        floor = 3,
                        occupied = true,
                        registerNumber = "BA54631"
                    },
                    new ParkingLotDetails
                    {
                        parkingSpaceNumber = "P2",
                        floor = 3,
                        occupied = true,
                        registerNumber = "DA46741"
                    },
                    new ParkingLotDetails
                    {
                        parkingSpaceNumber = "P3",
                        floor = 3,
                        occupied = true,
                        registerNumber = "QA13200"
                    });

                context.SaveChanges();
            }*/
        }
    }
}
