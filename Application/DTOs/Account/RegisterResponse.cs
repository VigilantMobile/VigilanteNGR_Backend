using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTOs.Account
{
    public class RegisterResponse
    {
        public bool UserAlreadyExists { get; set; }
        public string Message { get; set; }
    }
}
