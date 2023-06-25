using EtarChallenge.Dto.Auth;
using EtarChallenge.Services.Auth;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace EtarChallenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private IAuthService AuthService;
        private IValidator<LoginDto> validator;

        public AuthController(IAuthService _authService, IValidator<LoginDto> _validator)
        {
            validator = _validator;
            AuthService = _authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenDto?>> Login([FromBody] LoginDto data)
        {
            ValidationResult result = await validator.ValidateAsync(data);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var res = await AuthService.Login(data.username, data.password);
            return res != null ? Ok(res) : Unauthorized();
        }
    }
}