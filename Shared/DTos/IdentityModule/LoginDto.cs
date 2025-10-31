using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTos.IdentityModule
{
    public record LoginDto
    {

       public string Email { get; init; } = string.Empty;
       public string Password { get; init; } = string.Empty;
    }
    
}
