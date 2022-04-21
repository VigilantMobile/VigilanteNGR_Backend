using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTOs.Account
{
    public class UpdateProfileRequest
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        [RegularExpression(@"^([0-9]{11})$", ErrorMessage = "Invalid Mobile Number. Phone number must be 11 characters.")]
        public string PhoneNumber { get; set; }
        public int TownId { get; set; }
        public int SettlementId { get; set; }
    }
}
