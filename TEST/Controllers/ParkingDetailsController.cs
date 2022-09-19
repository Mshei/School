using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using ParkingCaseOne;
using ParkingCaseOne.Model;

namespace ParkingCaseOne.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParkingDetailsController : Controller
    {
        private ParkingLotDetailsDBContext _context;

        public ParkingDetailsController(ParkingLotDetailsDBContext context)
        {
            _context = context;
        }

        /*[HttpGet]
        public IActionResult Index()
        {
            var parkingLots = _context.CarDetails.ToList();
            return View(parkingLots);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ParkingLotDetails _lotDetails)
        {
            //Determine the next ID
            var newID = _context.CarDetails.Select(x => x.registerNumber).Max() + 1;
            _lotDetails.registerNumber = newID;

            _context.CarDetails.Add(_lotDetails);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _context.CarDetails.Remove(_context.CarDetails.Find(id));
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var game = _context.CarDetails.Find(id);
            return View(game);
        }*/

        [HttpPost]
        public IActionResult Edit(ParkingLotDetails _lotDetails)
        {
            _context.CarDetails.Update(_lotDetails);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Get()
        {
            String json;


            Console.WriteLine("TEST");

            return new OkObjectResult(true);
        }
    }

}