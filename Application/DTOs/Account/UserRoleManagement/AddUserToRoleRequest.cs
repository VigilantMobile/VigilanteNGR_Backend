using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Account.UserRoleManagement
{
    public class AddUserToRoleRequest
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public IEnumerable<RolestoConfigure> Roles { get; set; }
    }

    public class RolestoConfigure
    {
        [Required]
        public string RoleId { get; set; }

        [Required]
        public string RoleName { get; set; }

        [Required]
        public bool Selected { get; set; }
    }


}
