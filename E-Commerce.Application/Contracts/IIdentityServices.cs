using E_Commerce.Application.Common;
using E_Commerce.Application.DTOs.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Contracts
{
    public interface IIdentityServices
    {

        // Login & Registration
        Task<Result<IdentityUserResult>> FindByEmailAsync(string email, CancellationToken ct = default);

        Task<Result<bool>> CheckPasswordAsync(string email,string password, CancellationToken ct = default);

        Task<Result<IdentityUserResult>> CreateUser(RegisterDto registerDto, CancellationToken ct = default);

        Task<Result<IEnumerable<string>>> GetRolsAsync(string email);

        Task<Result<AddressDto>> GetAddressByEmailAsync(string email , CancellationToken ct = default);

        Task<Result<AddressDto>> UpdateAddressAsync(string email,AddressDto addressDto, CancellationToken ct = default);

        Task<Result<bool>> EmailExistsAsync(string email, CancellationToken ct = default);


    }
}
