using Domain.Common;
using Domain.Entities.AppTroopers.SecurityTip;
using Infrastructure.Persistence.Models.Identity;
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
        public string NPFAddress { get; set; }
        public string NPFPhone { get; set; }
        [ForeignKey("ApplicationUser")]
        public string TownSuperAdminId { get; set; }
        public int? SecurityTipId { get; set; }

        public virtual SecurityTip SecurityTip { get; set; }
        public virtual ICollection<ApplicationUser> NPFOperators { get; set; }
        public virtual ICollection<ApplicationUser> NPFAdmins { get; set; }
        public virtual ICollection<ApplicationUser> VigilanteOperators { get; set; }
        public virtual ICollection<ApplicationUser> VigilanteAdmins { get; set; }
    }
}
