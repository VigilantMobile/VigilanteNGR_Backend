using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Account
{
    public class CustomerRegisterRequest
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [RegularExpression(@"^([0-9]{11})$", ErrorMessage = "Invalid Mobile Number. Phone number must be 11 characters.")]
        public string PhoneNumber { get; set; }

        public string Password { get; set; }


        [Compare("Password")]
        public string ConfirmPassword { get; set; }


        [Required]
        public int TownId { get; set; }

        public int SettlementId { get; set; }
    }

    public class StaffRegisterRequest
    {
        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [RegularExpression(@"^([0-9]{11})$", ErrorMessage = "Invalid Mobile Number. Phone number must be 11 characters.")]
        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public List<string> RoleIds { get; set; }
    }
}
