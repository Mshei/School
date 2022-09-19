using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AssignmentOne.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardgameController : Controller
    {
        [HttpGet(Name = "SelectRandomCard")]
        public IActionResult Get()
        {
            CardRepository cardRepository = new CardRepository();
            string card;
            string color;
            string result;
            Random rand = new Random();
            string json = "";
            int index = rand.Next(cardRepository.cards.Length);

            card = cardRepository.cards[index];
            if (card != "JOKER")
            {
                index = rand.Next(cardRepository.colors.Length);
                color = cardRepository.colors[index];
                result = card + " Of " + color;
                json = JsonConvert.SerializeObject(result);


            }
            else
            {
                return new OkObjectResult(card); ;

            }
            return Ok(json);
        }
    }
}
