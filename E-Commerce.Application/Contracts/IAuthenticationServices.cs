using E_Commerce.Application.Common;
using E_Commerce.Application.DTOs.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Contracts
{
    public interface IAuthenticationServices
    {
        Task<Result<UserDto>> LoginAsync(LoginDto loginDto , CancellationToken ct = default);
        Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto, CancellationToken ct = default);

        Task<Result<bool>> CheckEmailAsync(string email, CancellationToken ct = default);

        Task<Result<AddressDto>> GetUserAddressAsync(string email, CancellationToken ct = default);

        Task<Result<AddressDto>> UpdateUserAddressAsync(AddressDto addressDto,string email, CancellationToken ct = default);

        Task<Result<UserDto>> GetCurrentAsync(string email, CancellationToken ct = default);

    }
}
