using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Models
{
    public class LGAPolygons
    {
        public string type { get; set; }
        public Crs crs { get; set; }
        public Feature[] features { get; set; }
    }

    public class Crs
    {
        public string type { get; set; }
        public Properties properties { get; set; }
    }

    public class Properties
    {
        public string name { get; set; }
    }

    public class Feature
    {
        public string type { get; set; }
        public Geo geometry { get; set; }
        public Properties1 properties { get; set; }
    }

    public class Geo
    {
        public string type { get; set; }
        public dynamic coordinates { get; set; }
        //public object[][][] coordinates { get; set; }

        //public List<List<List<double>>> coordinates { get; set; }
    }

    public class Properties1
    {
        public int ID_0 { get; set; }
        public string ISO { get; set; }
        public string NAME_0 { get; set; }
        public int ID_1 { get; set; }
        public string NAME_1 { get; set; }
        public int ID_2 { get; set; }
        public string NAME_2 { get; set; }
        public object HASC_2 { get; set; }
        public int CCN_2 { get; set; }
        public object CCA_2 { get; set; }
        public string TYPE_2 { get; set; }
        public string ENGTYPE_2 { get; set; }
        public object NL_NAME_2 { get; set; }
        public string VARNAME_2 { get; set; }
    }

}
