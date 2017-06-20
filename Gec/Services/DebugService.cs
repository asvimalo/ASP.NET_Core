using Gec.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gec.Services
{
    public class DebugMailService : IEmailService
    {
        public void SendMeil(string to, string from, string subject, string body)
        {
            Debug.WriteLine($"Sending Mail: To: {to} From: {from} Subjeect: {subject}");
        }
    }
}
