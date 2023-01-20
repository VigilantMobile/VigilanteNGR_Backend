using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Shared.Services.Notification.SMSHelperClasses
{
    public class SMSResponse
    {
        public string request_id { get; set; }
        public string status { get; set; }
        public DateTime created_at { get; set; }
    }
}
