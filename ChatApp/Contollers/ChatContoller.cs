using ChatApp.Contracts;
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
        private readonly ILogger<ChatController> _logger;

        public ChatController(ChatService chatService, ILogger<ChatController> logger)
        {
            _chatService = chatService;
            _logger = logger;
        }


        [HttpGet]
        [Route("messages")]
       public async Task<IActionResult> GetMessagesAsync(GetMessagesReqDTO _data)
        {
            _logger.LogInformation("Getting messages");
            var result = await _chatService.getAllMessagesAsync(_data.recipientId, _data.senderId);
            return Ok(result);
        }
    }
}
