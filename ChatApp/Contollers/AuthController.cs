using ChatApp.Contracts;
using ChatApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDTO loginData)
        {
            var result = await _authService.LoginAsync(loginData);
            if(result== null)
            {
                return Unauthorized("Invalid login or password");
            }
            return Ok(result);

        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var result = await _authService.RegisterAsync(registerDTO);
            if(result== null)
            {
                return StatusCode(500, "User already exists");
            }
            return Ok(result);
        }
    }
}
