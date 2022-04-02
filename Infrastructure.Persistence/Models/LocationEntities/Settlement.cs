using Domain.Common;
using Domain.Entities.AppTroopers.SecurityTip;
using Infrastructure.Persistence.Models.Identity;
using Infrastructure.Persistence.Models.Identity.Location;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Spatial;
using System.Text;

namespace Infrastructure.Persistence.Models.LocationEntities
{
    public class Settlement : AuditableBaseEntity
    {
        public string Name { get; set; }

        [ForeignKey("Town")]
        public int TownId { get; set; }
        public Town Town { get; set; }
        public string NPFSettlementAuthorityAddress { get; set; }
        public string AuthorityPhone { get; set; }

        [ForeignKey("NPFSettlementAdmin")]
        public string NPFTownAdminId { get; set; }
        public NPFSettlementAdmin NPFTownAdmin { get; set; }

        // VGNGA Users
        public virtual ICollection<ApplicationUser> VigilanteNGAdmins { get; set; }
        public virtual ICollection<ApplicationUser> VigilanteNGOperators { get; set; }

        // NPF Users
        public virtual ICollection<NPFSettlementAdmin> NPFSettlementAdmins { get; set; }
        public virtual ICollection<NPFSettlementOperator> NPFSettlementOperators { get; set; }

        // Official Vigilante Users
        public virtual ICollection<OfficialVigilanteSettlementAdmin> OfficialVigilanteSettlementAdmins { get; set; }
        public virtual ICollection<OfficialVigilanteSettlementOperator> OfficialVigilanteSettlementOperators { get; set; }

        public ICollection<SecurityTip> SecurityTips { get; set; }

        public Geometry Boundary { get; set; }
    }
}
