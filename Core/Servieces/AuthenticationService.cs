using Domain.Entites.IdentitiyModule;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Servces.Abstraction;
using Shared.DTos.IdentityModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal class AuthenticationService(UserManager<User> userManager) : IAuthenticationService
    {
        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user is null)
            {
                throw new UnauthorizedException();
            }
            var result = await userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result)
            {
                throw new UnauthorizedException();
            }
            return new UserResultDto(user.DisplayName, user.Email, "token");
        }



        public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new User
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                PhoneNumber = registerDto.PhoneNumber
            };
            var result= await userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new ValidationException(errors);

            }
            return new UserResultDto(user.DisplayName, user.Email, "token");    

        }
    }
}
