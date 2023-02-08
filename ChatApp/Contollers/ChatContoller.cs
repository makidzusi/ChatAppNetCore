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

        public ChatController(ChatService chatService)
        {
            _chatService = chatService;
        }


        [HttpGet]
        [Route("messages")]
       public async Task<IActionResult> GetMessagesAsync(GetMessagesReqDTO _data)
        {
            var result = await _chatService.getAllMessagesAsync(_data.recipientId, _data.senderId);
            return Ok(result);
        }
    }
}
