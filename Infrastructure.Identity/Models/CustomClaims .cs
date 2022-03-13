using Domain.Common;
using Infrastructure.Identity.Contexts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Models
{


    public class CustomClaims : AuditableBaseEntity
    {
        public string type { get; set; }
        public string value { get; set; }
        public string FriendlyName { get; set; }
    }
}
