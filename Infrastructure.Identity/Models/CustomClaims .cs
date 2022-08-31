using Domain.Common;

namespace Infrastructure.Identity.Models
{


    public class CustomClaims : AuditableBaseEntity
    {
        public string type { get; set; }
        public string value { get; set; }
        public string FriendlyName { get; set; }
    }
}
