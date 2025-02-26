using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Shared.Services.Notification.SMSHelperClasses
{
    public class SMSMessage
    {
        public string channel { get; set; }
        public string originator { get; set; }
        public List<string> recipients { get; set; }
        public string content { get; set; }
        public string data_coding { get; set; }
    }

    public class SMSRequest
    {
        public List<SMSMessage> messages { get; set; }
    }


}
