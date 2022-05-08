using NetTopologySuite.Geometries;

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
