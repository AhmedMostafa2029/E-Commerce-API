using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.DTOs.Identity
{
    public class UserDto
    {
        public string DisplayName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;

    }
}
