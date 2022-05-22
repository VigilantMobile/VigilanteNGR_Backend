using System;
using System.Collections.Generic;

namespace Application.Features.Location
{
    public class GetStateViewModel
    {
        public string StateName { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastModified { get; set; }
    }

    public class GetAllLGAsinStateViewModel
    {
        public int StateId { get; set; }
        public string State { get; set; }
        public int Count { get; set; }
        public List<LGAViewModel> LGAs { get; set; }
    }

    public class LGAViewModel
    {
        public int Id { get; set; }
        public string LGAName { get; set; }
        public DateTime Created { get; set; }
    }
}
