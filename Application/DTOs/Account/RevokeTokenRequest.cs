using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Account
{
    public class RevokeTokenRequest
    {
        public string Token { get; set; }
    }

    public class RefreshTokenRequest
    {
        public string Token { get; set; }
    }
}
