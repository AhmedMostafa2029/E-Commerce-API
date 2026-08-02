using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Services
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly IIdentityServices identityServices;
        private readonly ITokenServices tokenServices;

        public AuthenticationServices(IIdentityServices identityServices, ITokenServices tokenServices)
        {
            this.identityServices = identityServices;
            this.tokenServices = tokenServices;
        }


        public async Task<Result<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken ct = default)
        {
            var UserResult = await identityServices.FindByEmailAsync(loginDto.Email, ct);
            if (!UserResult.IsSuccess)
            {
                return Result<UserDto>.Fail(UserResult.Errors);
            }

            var PasswordCheck = await identityServices.CheckPasswordAsync(loginDto.Email, loginDto.Password, ct);
            if (!PasswordCheck.IsSuccess)
            {
                return Result<UserDto>.Fail(Error.UnAuthorized("Invalid Email Or Password"));
            }


            var roles = await identityServices.GetRolsAsync(UserResult.data.Email);
            var token = tokenServices.CreateToken(UserResult.data.Id, UserResult.data.Email, UserResult.data.UserName, roles.data);
            return Result<UserDto>.Ok(new UserDto
            {
                DisplayName = UserResult.data.DisplayName,
                Email = UserResult.data.Email,
                Token = token // Replace with actual token generation logic
            });
        }

        public async Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto, CancellationToken ct = default)
        {
            var result = await identityServices.CreateUser(registerDto, ct);

            if(!result.IsSuccess || result.data is null)
            {
                return Result<UserDto>.Fail(result.Errors);
            }


            var roles = await identityServices.GetRolsAsync(result.data.Email);
            var token = tokenServices.CreateToken(result.data.Id, result.data.Email, result.data.UserName, roles.data);
            return Result<UserDto>.Ok(new UserDto
            {
                DisplayName = result.data.DisplayName,
                Email = result.data.Email,
                Token = token // Replace with actual token generation logic
            });

        }

        public async Task<Result<bool>> CheckEmailAsync(string email, CancellationToken ct = default)
        {
            return await identityServices.EmailExistsAsync(email, ct);
        }

        public async Task<Result<UserDto>> GetCurrentAsync(string email, CancellationToken ct = default)
        {
            var userResult = await identityServices.FindByEmailAsync(email, ct);

            if (!userResult.IsSuccess)
                return Result<UserDto>.Fail(userResult.Errors);

            var user = userResult.data;
            var roleResult = await identityServices.GetRolsAsync(user.Email);

            if (!roleResult.IsSuccess)
                return Result<UserDto>.Fail(roleResult.Errors);

            var token = tokenServices.CreateToken(user.Id, user.Email, user.UserName, roleResult.data);

            return Result<UserDto>.Ok(new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = token // Replace with actual token generation logic
            });

        }

        public async Task<Result<AddressDto>> GetUserAddressAsync(string email, CancellationToken ct = default)
        {
            var result = await identityServices.GetAddressByEmailAsync(email, ct);

            if(!result.IsSuccess)
            {
                return Result<AddressDto>.Fail(result.Errors);
            }

            return Result<AddressDto>.Ok(result.data);


        }

        public async Task<Result<AddressDto>> UpdateUserAddressAsync(AddressDto addressDto, string email, CancellationToken ct = default)
        {
            return await identityServices.UpdateAddressAsync(email, addressDto, ct);
        }
    }
}
