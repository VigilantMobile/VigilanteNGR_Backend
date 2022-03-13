using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Account
{
    public class CreateCustomClaimsModel
    {
        [Required]
        public string ClaimType { get; set; }
        [Required]
        public string ClaimValue { get; set; }

    }
}
