using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.CompanyEntities
{
    public class Department : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        [ForeignKey("ApplicationUser")]
        public string HodId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string SecretaryId { get; set; }
    }
}
