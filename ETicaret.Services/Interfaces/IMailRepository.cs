using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Services.Interfaces
{
    public interface IMailRepository
    {
        Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true);
    }
}
