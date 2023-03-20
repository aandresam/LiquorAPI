using Microsoft.AspNetCore.Mvc;
using liquorApi.Services;
using liquorApi.Models;
using liquorApi.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace liquorApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly AuthenticationService authService;

        public AuthController(AuthenticationService authService)
        {
            this.authService = authService;
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] UserLoginDtoIn loginDtoIn)
        {
            var response = this.authService.Authenticate(loginDtoIn);

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDtoIn registerDtoIn)
        {
            var response = await this.authService.Register(registerDtoIn);
            
            return Ok(response);
        }
    }
}