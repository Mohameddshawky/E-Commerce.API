using Shared.DTos.IdentityModule;
using Shared.DTos.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IAuthenticationService
    {
        Task<UserResultDto> LoginAsync(LoginDto loginDto);
        Task<UserResultDto> RegisterAsync(RegisterDto registerDto);

        Task<UserResultDto> GetCurrentUserAsync(string userEmail);
        Task<bool> CheckEmailExistAsync(string userEmail);
          
        Task<AddressDto>GetUserAddressAsync(string userEmail);
        Task<AddressDto>UpdateUserAddressAsync(string userEmail,AddressDto addressDto);
    }
}
