﻿using System;
using System.Collections.Generic;
using System.Text;

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
        public string StateId { get; set; }
        public string State { get; set; }
        public int Count { get; set; }
        public List<LGAViewModel> LGAs { get; set; }
    }

    public class LGAViewModel
    {
        public string Id { get; set; }
        public string LGAName { get; set; }
        public DateTime Created { get; set; }
    }
}
