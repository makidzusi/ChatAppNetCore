using ChatApp.Contracts;
using ChatApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ChatApp.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(AuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDTO loginData)
        {
            _logger.LogInformation("User with credentials : {@loginDAta} trying to login", loginData);

            var result = await _authService.LoginAsync(loginData);
            if(result== null)
            {
                _logger.LogInformation("User with credentials : {@loginDAta} failed auth", loginData);
                return Unauthorized("Invalid login or password");
            }

            _logger.LogInformation("User with credentials : {@loginDAta} logged in", loginData);
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

        [HttpPost]
        [Authorize(Policy = "Email")]
        [Route("getme")]
        public async Task<IActionResult> GetMeAsync()
        {
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value, options);
            return Ok(json);
        }
    }
}
