using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserProfile.Customer.Queries.GetCustomerProfile
{
    public class CustomerProfileViewModel
    {
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string DateOfBirth { get; set; }
        public string LocationLevel { get; set; }
        [Required]
        public List<CustomerTrustedContactViewModel> CustomerTrustedContacts { get; set; }
        public CustomerLocationViewModel CustomerLocation { get; set; }
    }

    public class CreateCustomerTrustedContactViewModel
    {
        public string CustomerId { get; set; }

        public List<CustomerTrustedContactViewModel> customerTrustedContacts { get; set; }
    }


    public class CustomerTrustedContactViewModel
    {
        public string FullName { get; set; }
        public Guid TownId { get; set; }
        public string FullAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
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
