using System;
using System.Collections.Generic;

namespace Application.Features.Location
{

    public class GetLGAViewModel
    {
        public string LGAName { get; set; }
        public string State { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastModified { get; set; }

    }

    public class GetAllDistrictsinLGAViewModel
    {
        public string LGAId { get; set; }
        public string LGA { get; set; }
        public int Count { get; set; }

        public List<DistrictViewModel> Districts { get; set; }
    }

    public class DistrictViewModel
    {
        public string Id { get; set; }
        public string DistrictName { get; set; }
        public DateTime Created { get; set; }
    }
}
