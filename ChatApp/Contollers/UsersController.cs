using ChatApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Contollers
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly UserService _userService;
        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetUsersListAsync()
        {
            var result = await _userService.GetUsersListAsync();
            return Ok(result);
        }

    }
}
