using System;

namespace Application.Features.AppTroopers.SecurityTip.Queries.GetAllSecurityTipCategories
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
