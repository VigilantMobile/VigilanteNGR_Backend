using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Account
{
    public class CustomerRegisterRequest
    {
        [Required]
        public string FullName { get; set; }
        //public string MiddleName { get; set; }

        //[Required]
        //public string LastName { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^([0-9]{11})$", ErrorMessage = "Invalid Mobile Number. Phone number must be 11 characters.")]
        public string PhoneNumber { get; set; }

        public string Password { get; set; }


        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        //[Required]
        public string TownId { get; set; }

        [RegularExpression("^[-+]?([1-8]?\\d(\\.\\d+)?|90(\\.0+)?),\\s*[-+]?(180(\\.0+)?|((1[0-7]\\d)|([1-9]?\\d))(\\.\\d+)?)$", ErrorMessage = "Enter valid Latitude and Longitude, seperated by a comma: (e.g. 43.478685,-80.523607)")]
        //[RegularExpression("^-?[0-9]{1,3}(?:\\.[0-9]{1,10})?$", ErrorMessage = "Enter valid Latitude and Longitude, seperated by a comma: (e.g. 43.478685,-80.523607")]

        [Required]
        public string locationCoordinates { get; set; }

        [Required]
        public string Address { get; set; }

        //public string SettlementId { get; set; }
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
