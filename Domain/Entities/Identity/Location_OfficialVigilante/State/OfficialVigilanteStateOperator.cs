using Domain.Entities.Identity;
using Domain.Entities.LocationEntities;
using System.Collections.Generic;

namespace Domain.Entities.Identity.Identity
{
    public class OfficialVigilanteStateOperator : ApplicationUser
    {
        public virtual ICollection<State> States { get; set; }
    }
}
