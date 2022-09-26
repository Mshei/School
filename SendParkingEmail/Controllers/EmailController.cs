using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [HttpPost]
        public bool SendEmail(EmailData emailData)
        {
            return _emailService.SendEmail(emailData);
        }
    }
}
