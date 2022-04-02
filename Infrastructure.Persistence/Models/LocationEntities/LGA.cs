using Domain.Common;
using Domain.Entities.AppTroopers.SecurityTip;
using Infrastructure.Persistence.Models.Identity;
using Infrastructure.Persistence.Models.Identity.Location;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Infrastructure.Persistence.Models.LocationEntities
{
    public class LGA : AuditableBaseEntity
    {
        public string Name { get; set; }

        [ForeignKey("State")]
        public int StateId { get; set; }
        public State State { get; set; }

        [ForeignKey("NPFLGAAdmin")]
        public string NPFLGAAdminId { get; set; }
        public NPFLGAAdmin NPFLGAAdmin { get; set; }

        public string NPFLGAAuthorityAddress { get; set; }
        public string NPFPhone { get; set; }
        public bool isCapital { get; set; }

        // VGNGA Users
        public virtual ICollection<ApplicationUser> VigilanteNGAdmins { get; set; }
        public virtual ICollection<ApplicationUser> VigilanteNGOperators { get; set; }

        // NPF Users
        public virtual ICollection<NPFLGAAdmin> NPFLGAAdmins { get; set; }
        public virtual ICollection<NPFLGAOperator> NPFLGAOperators { get; set; }

        // Official Vigilante Users
        public virtual ICollection<OfficialVigilanteLGAAdmin> OfficialVigilanteLGAAdmins { get; set; }
        public virtual ICollection<OfficialVigilanteLGAOperator> OfficialVigilanteLGAOperators{ get; set; }

        public ICollection<SecurityTip> SecurityTips { get; set; }

        public Geometry Boundary { get; set; }
    }
}
