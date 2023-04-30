using ETicaret.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Services.Concrete
{
    public class EFMailRepository : IMailRepository
    {
        private readonly IConfiguration _configuration;

        public EFMailRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
          
            MailMessage message = new MailMessage();
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = isBodyHtml;
            message.To.Add(to);
            message.From = new(_configuration["Mail:Username"], "Samet");

            SmtpClient client = new SmtpClient();

            client.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
            client.Port = 587;
            client.EnableSsl = true;
            client.Host = _configuration["Mail:Host"];

           await client.SendMailAsync(message);

       

        }
    }
}
