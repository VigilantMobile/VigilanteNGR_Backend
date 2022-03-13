using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Services
{
    public interface IDbInitializer
    {
        void Initialize();
        void SeedStatesandLGAs();
        void SeedAppTrooperHelpers();
    }
}
