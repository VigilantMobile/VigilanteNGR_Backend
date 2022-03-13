using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Account.UserRoleManagement
{
    public class GetStaffWithRolesRequest
    {

        [Required]
        public string userId { get; set; }
    }
}
