using Infrastructure.Persistence.Models.LocationEntities;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models.Identity.Location
{
    public class OfficialVigilanteStateOperator : ApplicationUser
    {
        public virtual ICollection<State> States { get; set; }
    }
}
