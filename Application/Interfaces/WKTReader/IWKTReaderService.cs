using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IKWKTReaderService
    {
        bool IsValidWKT(string WKT);
    }
}
