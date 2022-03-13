using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Account.RoleManagement
{
    public class CreateRoleRequest
    {
        [Required]
        public string role { get; set; }
    }
}
