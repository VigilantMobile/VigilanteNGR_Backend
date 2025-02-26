using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Account
{
    public class VerifyOTPRequest
    {
        [RegularExpression(@"^([0-9]{11})$", ErrorMessage = "Invalid Mobile Number. Phone number must be 11 characters.")]
        public string PhoneNumber { get; set; }

        public string OTP { get; set; }

    }

    public class ResendOTPRequest
    {
        [RegularExpression(@"^([0-9]{11})$", ErrorMessage = "Invalid Mobile Number. Phone number must be 11 characters.")]
        public string PhoneNumber { get; set; }

    }
}
