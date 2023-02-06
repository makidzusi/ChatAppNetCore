using ChatApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Contollers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ChatService _chatService;

        public ChatController(ChatService chatService)
        {
            _chatService = chatService;
        }


        [HttpPost]
        [Route("send")]
        public async Task<IActionResult> SendMessageAsync()
        {
            return Ok();
        }
    }
}
