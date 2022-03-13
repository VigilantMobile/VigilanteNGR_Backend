using Domain.Common;
using Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Models
{


    public class CustomClaims : AuditableBaseEntity
    {
        public string type { get; set; }
        public string value { get; set; }
        public string FriendlyName { get; set; }
    }
}
