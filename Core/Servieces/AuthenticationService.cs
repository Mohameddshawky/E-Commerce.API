using AutoMapper;
using Domain.Entites.IdentitiyModule;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstraction;
using Shared.Common;
using Shared.DTos.IdentityModule;
using Shared.DTos.OrderModule;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Services
{
    internal class AuthenticationService(
        UserManager<User> userManager,
        IOptions<JwtOptions> options,
        IMapper mapper

        ) : IAuthenticationService
    {
        public async Task<bool> CheckEmailExistAsync(string userEmail)
        {
            var user =await userManager.FindByEmailAsync(userEmail);
            return  user is not null;                                                                                           
        }

        public async Task<UserResultDto> GetCurrentUserAsync(string userEmail)
        {
            var user=await userManager.FindByEmailAsync(userEmail)
                ?? throw new UserNotFoundException(userEmail);
            return new UserResultDto(user.DisplayName, user.Email, await GenerateTokenAsync(user));
        }

        public async Task<AddressDto> GetUserAddressAsync(string userEmail)
        {
            var user=await userManager.Users.Include(u=>u.Address)
                .FirstOrDefaultAsync(u=>u.Email==userEmail)??throw new UserNotFoundException(userEmail);
            return mapper.Map<AddressDto>(user.Address);
        }

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
            return new UserResultDto(user.DisplayName, user.Email, await GenerateTokenAsync(user));
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
            return new UserResultDto(user.DisplayName, user.Email, await GenerateTokenAsync(user));    

        }

        public async Task<AddressDto> UpdateUserAddressAsync(string userEmail, AddressDto addressDto)
        {
            var user = await userManager.Users.Include(u => u.Address)
                        .FirstOrDefaultAsync(u => u.Email == userEmail) ?? throw new UserNotFoundException(userEmail);
            if(user.Address is not null)
            {
                user.Address.FirstName = addressDto.FirstName;
                user.Address.LastName = addressDto.LastName;    
                user.Address.Street = addressDto.Street;
                user.Address.City = addressDto.City;
                user.Address.Country = addressDto.Country;
            }
            else
               user.Address = mapper.Map<Address>(addressDto);

            
            await userManager.UpdateAsync(user);
            return mapper.Map<AddressDto>(user.Address);
        }

        private async Task<string> GenerateTokenAsync(User user)
        {
            var JwtOptions= options.Value;
            var claims = new List<Claim>
           {
                new Claim(ClaimTypes.Email,user.Email!),
                new Claim(ClaimTypes.Name,user.DisplayName)
           };
            var roles = await userManager.GetRolesAsync(user);
            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));   
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.SecretKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new JwtSecurityToken
                (
                issuer: JwtOptions.Issuer,
                audience: JwtOptions.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(JwtOptions.ExpirationInDays),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);                           

        }   
    }
}
