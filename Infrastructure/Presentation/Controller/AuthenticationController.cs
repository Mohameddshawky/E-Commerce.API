using Microsoft.AspNetCore.Mvc;
using Servces.Abstraction;
using Shared.DTos.IdentityModule;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
