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
    public class Town : AuditableBaseEntity
    {
        public string Name { get; set; }

        [ForeignKey("LGA")]
        public int LGAId { get; set; }
        public LGA LGA { get; set; }
        public string NPFTownAuthorityAddress { get; set; }
        public string NPFPhone { get; set; }


        // VGNGA Users
        public virtual ICollection<ApplicationUser> VigilanteNGAdmins { get; set; }
        public virtual ICollection<ApplicationUser> VigilanteNGOperators { get; set; }

        // NPF Users
        public virtual ICollection<NPFTownAdmin> NPFTownAdmins { get; set; }
        public virtual ICollection<NPFTownOperator> NPFTownOperators { get; set; }

        // Official Vigilante Users
        public virtual ICollection<OfficialVigilanteTownAdmin> OfficialVigilanteTownAdmins { get; set; }
        public virtual ICollection<OfficialVigilanteTownOperator> OfficialVigilanteTownOperators { get; set; }

        public ICollection<SecurityTip> SecurityTips { get; set; }

        public Geometry Boundary { get; set; }
    }
}
