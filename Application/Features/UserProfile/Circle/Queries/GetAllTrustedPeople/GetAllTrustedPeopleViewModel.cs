using System;

namespace Application.Features.UserProfile
{
    public class GetAllTrustedPeopleViewModel
    {
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
    }
}
