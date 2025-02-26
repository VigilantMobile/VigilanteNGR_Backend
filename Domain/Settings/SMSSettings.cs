using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Settings
{
    public class SMSSettings
    {
        public string D7APISMSBase { get; set; }
        public string D7SMSAPIToken { get; set; }
        public string D7SMSAPIKey { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
