using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserProfile
{
    public class CustomerProfileViewModel
    {
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }

        [Required, MinLength(1)]
        public List<CustomerTrustedContactViewModel> CustomerTrustedContacts { get; set; }
        public CustomerLocationViewModel CustomerLocation { get; set; }
    }

    public class CreateCustomerTrustedContactViewModel
    {
        public string CustomerId { get; set; }

        [Required, MinLength(1)]
        public List<CustomerTrustedContactViewModel> customerTrustedContacts { get; set; }
    }


    public class CustomerTrustedContactViewModel
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string TownId { get; set; }
        [Required]
        public string FullAddress { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        public string Gender { get; set; }
    }

    public class CustomerLocationViewModel
    {
        public CustomerStateViewModel CustomerState { get; set; }
        public CustomerLGAViewModel CustomerLGA { get; set; }
        public CustomerDistrictViewModel CustomerDistrict { get; set; }
    }

    public class CustomerStateViewModel
    {
        public string StateId { get; set; }
        public string StateName { get; set; }
    }

    public class CustomerLGAViewModel
    {
        public string LGAId { get; set; }
        public string LGAName { get; set; }
    }
    public class CustomerDistrictViewModel
    {
        public string DistrictId { get; set; }
        public string DistrictName { get; set; }
    }
}
