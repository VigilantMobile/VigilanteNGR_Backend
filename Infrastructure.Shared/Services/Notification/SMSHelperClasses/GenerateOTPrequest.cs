using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Shared.Services.Notification.SMSHelperClasses
{
    public class GenerateOTPrequest
    {
        public string originator { get; set; }
        public string recipient { get; set; }
        public string content { get; set; }
        public string expiry { get; set; }
        public string data_coding { get; set; }
    }

    public class GenerateOTPResponses
    {
        public string otp_id { get; set; }
        public string status { get; set; }
        public int expiry { get; set; }
    }
}
