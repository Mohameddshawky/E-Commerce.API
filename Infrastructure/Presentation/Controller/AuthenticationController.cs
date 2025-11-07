using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servces.Abstraction;
using Shared.DTos.IdentityModule;
using Shared.DTos.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    public class AuthenticationController(IServiceManger serviceManger):ApiController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserResultDto>> LoginAsync([FromBody]LoginDto loginDto)
        =>Ok(await serviceManger.authenticationService.LoginAsync(loginDto));

        [HttpPost("Register")]
        public async Task<ActionResult<UserResultDto>> RegisterAsync([FromBody]RegisterDto registerDto)
            =>Ok( await serviceManger.authenticationService.RegisterAsync(registerDto));

        [HttpGet("EmailExist")]
        public async Task<ActionResult<bool>> CheckEmailExistAsync([FromQuery]string email)
            =>Ok( await serviceManger.authenticationService.CheckEmailExistAsync(email));

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserResultDto>> GetCurrentUserAsync()
         {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user =await serviceManger.authenticationService.GetCurrentUserAsync(email);
            return Ok(user);

        }
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetUserAddressAsync()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var address = await serviceManger.authenticationService.GetUserAddressAsync(email);
            return Ok(address);
        }

        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddressAsync([FromBody]AddressDto addressDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var address = await serviceManger.authenticationService.UpdateUserAddressAsync(email, addressDto);
            return Ok(address);
        }
    }
}
