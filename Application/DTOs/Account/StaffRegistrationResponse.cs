using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Account
{
    public class StaffRegistrationResponse
    {
        public bool UserAlreadyExists { get; set; }
        public string Message { get; set; }
        public string VerificationUrl { get; set; }
    }
}
