using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SendParkingEmail.Model;
using SendParkingEmail.Services;

namespace SendParkingEmail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        IEmailService _emailService = null;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        [HttpGet]
        public IActionResult SendEmail(String _licensePlate, String _email)
        {
            EmailData emailData = new EmailData();

            if (_licensePlate != null)
            { 
                emailData.EmailToId = _email;
                emailData.EmailSubject = String.Format("Parking started for car owner {0}", _licensePlate);
                emailData.EmailToName = "";
                emailData.EmailBody = DateTime.Now.ToString("dd/MM/yyyy") + " New parking have started";
            }

            _emailService.SendEmail(emailData);

            string json;

            json = JsonConvert.SerializeObject(string.Format("Email sent to {0} regarding {1} ", emailData.EmailToId, emailData.EmailSubject));

            return new OkObjectResult(json);
        }
    }
}
