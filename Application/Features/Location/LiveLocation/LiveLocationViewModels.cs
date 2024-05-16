using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Location.LiveLocation
{
    public class LiveLocationViewModel
    {
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string LGAName { get; set; }
        public string DistrictName { get; set; }
        public string FormattedAddress { get; set; }
    }
}
