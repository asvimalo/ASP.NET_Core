using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gec.Services
{
    public interface IEmailService
    {
        void SendMeil(string to, string from, string subject, string body);
    }
}
