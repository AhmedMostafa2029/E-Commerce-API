using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Identity;
using E_Commerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;

namespace E_Commerce.Application.Services
{
    public class IdentityServices : IIdentityServices
    {
        private readonly UserManager<ApplicationUser> userManager;

        public IdentityServices(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<Result<bool>> CheckPasswordAsync(string email, string password, CancellationToken ct = default)
        {
            var user = await userManager.FindByEmailAsync(email);

            if(user == null)
            {
                return Result<bool>.Fail(Error.NotFound("User IS Not Found"));
            }
            var isvalid = await userManager.CheckPasswordAsync(user, password);
            return Result<bool>.Ok(isvalid);
        }

        public async Task<Result<IdentityUserResult>> CreateUser(RegisterDto registerDto, CancellationToken ct = default)
        {
            var user = new ApplicationUser()
            {
                Email = registerDto.Email,
                UserName = registerDto.Username,
                PhoneNumber = registerDto.PhoneNumber,
                DisplayName = registerDto.DisplayName
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);
            if(!result.Succeeded)
            {
                var errors = result.Errors.Select(e => new Error(e.Code, e.Description)).ToList();
                return Result<IdentityUserResult>.Fail(errors);
            }
            return Result<IdentityUserResult>.Ok(new IdentityUserResult(user.Id, user.Email, user.UserName, user.DisplayName));

        }


        public async Task<Result<IdentityUserResult>> FindByEmailAsync(string email, CancellationToken ct = default)
        {
            var user = await userManager.FindByEmailAsync(email);
            if(user == null)
            {
                return Result<IdentityUserResult>.Fail(Error.NotFound("User IS Not Found.."));
            }
            else
            {
                return Result<IdentityUserResult>.Ok(new IdentityUserResult(user.Id , user.Email , user.UserName , user.DisplayName));
            }
        }

        public async Task<Result<IEnumerable<string>>> GetRolsAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if(user == null)
            {
                return Result<IEnumerable<string>>.Fail(Error.NotFound("User IS Not Found.."));
            }
            var roles = await userManager.GetRolesAsync(user);
            return Result<IEnumerable<string>>.Ok(roles);
        }

        public async Task<Result<AddressDto>> GetAddressByEmailAsync(string email, CancellationToken ct = default)
        {
            var user = await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == email, ct);
            if(user == null)
            {
                return Result<AddressDto>.Fail(Error.NotFound("User IS Not Found.."));
            }
            var address = user.Address;
            if(address == null)
            {
                return Result<AddressDto>.Fail(Error.NotFound("Address IS Not Found.."));
            }
            var addressDto = new AddressDto
            {
                FirstName = address.FirstName,
                LastName = address.LastName,
                Street = address.Street,
                City = address.City,
                Countery = address.Countery
            };
            return Result<AddressDto>.Ok(addressDto);
        }

        public async Task<Result<bool>> EmailExistsAsync(string email, CancellationToken ct = default)
        {
            return await userManager.FindByEmailAsync(email) is not null
                ? Result<bool>.Ok(true)
                : Result<bool>.Ok(false);
        }
        public async Task<Result<AddressDto>> UpdateAddressAsync(string email, AddressDto addressDto, CancellationToken ct = default)
        {
            var user = await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == email, ct);
            if (user == null)
            {
                return Result<AddressDto>.Fail(Error.NotFound("User IS Not Found.."));
            }

            if (user.Address == null)
            {
                user.Address = new Address()
                {
                    FirstName = addressDto.FirstName,
                    LastName = addressDto.LastName,
                    Street = addressDto.Street,
                    City = addressDto.City,
                    Countery = addressDto.Countery,
                };
            }else
            {
                user.Address.FirstName = addressDto.FirstName;
                user.Address.LastName = addressDto.LastName;
                user.Address.Street = addressDto.Street;
                user.Address.City = addressDto.City;
                user.Address.Countery = addressDto.Countery;
            }

            var result = await userManager.UpdateAsync(user);
            if(!result.Succeeded)
            {
                return Result<AddressDto>.Fail(Error.Failure("Can not update user address."));
            }

            return Result<AddressDto>.Ok(addressDto);
        }
    }
}
