using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Identity
{


    public class CustomClaims : AuditableBaseEntity
    {
        public string type { get; set; }
        public string value { get; set; }
        public string FriendlyName { get; set; }
    }
}
