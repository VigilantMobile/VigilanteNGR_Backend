using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

namespace Application.Interfaces
{
    public class WKTReaderService : IKWKTReaderService
    {
        public bool IsValidWKT(string WKTText)
        {
            WKTReader reader = new WKTReader(GeometryFactory.Default);
            Geometry g = reader.Read(WKTText);

            bool isValid = g.IsValid == true ? true : false;

            return isValid;
        }

    }
}
