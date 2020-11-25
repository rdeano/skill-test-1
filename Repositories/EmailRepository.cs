using Microsoft.Extensions.Configuration;
using skill_test_1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace skill_test_1.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly IConfiguration _configuration;

        public EmailRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void sendEmail(string from, string to, string subject, string content)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(from);
            message.To.Add(new MailAddress(to));
            message.Subject = subject;
            message.IsBodyHtml = true; //to make message body as html  
            message.Body = content;

            smtp.Port = Convert.ToInt32(_configuration["Email:port"]);
            smtp.Host = _configuration["Email:host"];
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new NetworkCredential(_configuration["Email:uname"], _configuration["Email:pword"]);

            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }


    }
}
