using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.DTOs.Identity
{
    public class LoginDto
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
