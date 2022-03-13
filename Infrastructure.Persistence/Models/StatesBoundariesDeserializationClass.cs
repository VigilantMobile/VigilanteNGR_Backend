using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Infrastructure.Persistence.Models
{
    public class StatesMPs
    {
        public string admin1Name { get; set; }
        public string admin1Pcod { get; set; }
        public string admin1RefN { get; set; }
        public string admin0Name { get; set; }
        public string admin0Pcod { get; set; }
        public string date { get; set; }
        public string validOn { get; set; }
        public float Shape_Leng { get; set; }
        public float Shape_Area { get; set; }
        public string admin1AltN { get; set; }
        public string admin1Al_1 { get; set; }
        public string validTo { get; set; }
        public string WKT { get; set; }
    }
}
