using Application.Interfaces;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
