using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mensajeria.Email.@interface
{
    public interface IServiceEmail
    {
        Task EnviarHtmlAsync(string to, string subject, string htmlBody);
    }
}
