using BulletinBoard.Core.Interfaces;
using BulletinBoard.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                var user = await _userService.RegisterAsync(model);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                var user = await _userService.LoginAsync(model);
                var token = _userService.GenerateJwtToken(
                    user,
                    _configuration["Jwt:Key"]!,
                    _configuration["Jwt:Issuer"]!,
                    _configuration["Jwt:Audience"]!);

                return Ok(new { token, user });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
}
