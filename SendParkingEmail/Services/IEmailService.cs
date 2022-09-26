using SendParkingEmail.Model;

namespace SendParkingEmail.Services
{
    public interface IEmailService
    {
        bool SendEmail(EmailData emailData);
    }
}
