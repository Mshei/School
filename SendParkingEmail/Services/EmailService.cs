﻿using Microsoft.Extensions.Options;
using MimeKit;
using SendParkingEmail.Model;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SendParkingEmail.Configurations;
using MailKit;
using MailKit.Net.Smtp;

namespace SendParkingEmail.Services
{
    public class EmailService : IEmailService
    {
        EmailSettings _emailSettings = null;
        public EmailService(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }
        public bool SendEmail(EmailData emailData)
        {
            try
            {
                MimeMessage emailMessage = new MimeMessage();
                MailboxAddress emailFrom = new MailboxAddress(_emailSettings.Name, _emailSettings.EmailId);
                emailMessage.From.Add(emailFrom);
                MailboxAddress emailTo = new MailboxAddress(emailData.EmailToName, emailData.EmailToId);
                emailMessage.To.Add(emailTo);
                emailMessage.Subject = emailData.EmailSubject;
                BodyBuilder emailBodyBuilder = new BodyBuilder();
                emailBodyBuilder.TextBody = emailData.EmailBody;
                emailMessage.Body = emailBodyBuilder.ToMessageBody();
                SmtpClient emailClient = new SmtpClient();
                emailClient.Connect(_emailSettings.Host, _emailSettings.Port, _emailSettings.UseSSL);
                emailClient.Authenticate(_emailSettings.EmailId, _emailSettings.Password);
                emailClient.Send(emailMessage);
                emailClient.Disconnect(true);
                emailClient.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                //Log Exception Details
                return false;
            }
        }
    }
}
