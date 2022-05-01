using Domain.Common;
using Domain.Entities.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.CompanyEntities
{
    public class Department : AuditableBaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        [ForeignKey("HOD")]
        public string HodId { get; set; }
        public virtual ApplicationUser HOD { get; set; }

        [ForeignKey("Secretary")]
        public string SecretaryId { get; set; }
        public virtual ApplicationUser Secretary { get; set; }

        public ICollection<ApplicationUser> VGNGAStaff { get; set; }
    }
}
