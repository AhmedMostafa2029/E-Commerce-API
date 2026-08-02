using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    public class AuthenticationController : ApiBaseController
    {
        private readonly IAuthenticationServices authenticationServices;

        public AuthenticationController(IAuthenticationServices authenticationServices)
        {
            this.authenticationServices = authenticationServices;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto, CancellationToken ct = default)
        {
            return ToActionResult(await authenticationServices.LoginAsync(loginDto, ct));
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto , CancellationToken ct)
        {
            return ToActionResult(await authenticationServices.RegisterAsync(registerDto, ct));
        }

        [HttpGet("emailExists")]
        public async Task<ActionResult<bool>> CheckEmail([FromQuery] string email, CancellationToken ct)
        {
            return ToActionResult(await authenticationServices.CheckEmailAsync(email, ct));
        }

        [HttpGet("currentUser")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser([FromQuery] string email, CancellationToken ct)
        {
            return ToActionResult(await authenticationServices.GetCurrentAsync(email, ct));
        }

        [HttpGet("address")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> GetUserAddress([FromQuery] string email, CancellationToken ct)
        {
            return ToActionResult(await authenticationServices.GetUserAddressAsync(email, ct));
        }

        [HttpPost("address")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress([FromQuery] string email, [FromBody] AddressDto addressDto, CancellationToken ct)
        {
            return ToActionResult(await authenticationServices.UpdateUserAddressAsync(addressDto,email, ct));
        }


    }
}
