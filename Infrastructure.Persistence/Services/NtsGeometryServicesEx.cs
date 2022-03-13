using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Services
{
    public class NtsGeometryServicesEx : NetTopologySuite.NtsGeometryServices
    {
        public NtsGeometryServicesEx() { }

        public NtsGeometryServicesEx(PrecisionModel precisionModel, int srid)
        {
        }


        protected override GeometryFactory CreateGeometryFactoryCore(
            PrecisionModel precisionModel,
            int srid,
            CoordinateSequenceFactory coordinateSequenceFactory)
        {
            return new GeometryFactoryEx(precisionModel, srid, coordinateSequenceFactory);
        }
    }
}
