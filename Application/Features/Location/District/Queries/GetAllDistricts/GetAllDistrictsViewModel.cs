using System;

namespace Application.Features.Location
{
    public class GetAllDistrictsViewModel
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastModified { get; set; }
    }

    public class GetDistrictViewModel
    {
        public string DistrictName { get; set; }
        public string LGA { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
