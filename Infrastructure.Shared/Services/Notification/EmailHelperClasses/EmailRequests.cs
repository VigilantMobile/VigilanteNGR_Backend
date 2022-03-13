using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Shared.Services.Notification.EmailHelperClasses
{
    public class WelcomeMailRequest
    {
        public string ToEmail { get; set; }
        public string UserName { get; set; }
    }

    public class GenericMailRequest
    {
        public string ToEmail { get; set; }
        public string UserName { get; set; }
        public string Heading { get; set; }
        public string Paragraph1 { get; set; }
        public string Paragraph2 { get; set; }
    }
}
