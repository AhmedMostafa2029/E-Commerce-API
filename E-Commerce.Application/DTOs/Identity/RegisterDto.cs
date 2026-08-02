using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.DTOs.Identity
{
    public class RegisterDto
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
    }
}
