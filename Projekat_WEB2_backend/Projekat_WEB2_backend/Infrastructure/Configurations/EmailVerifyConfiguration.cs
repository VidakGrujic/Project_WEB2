using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend.Infrastructure.Configurations
{
    public class EmailVerifyConfiguration
    {
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string MessageAccepted { get; set; }
        public string MessageDenied { get; set; }
        public string Subject { get; set; }
    }
}
