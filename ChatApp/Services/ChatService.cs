using ChatApp.DataAccess;
using ChatApp.DataAccess.Entities;
using ChatApp.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Services
{
    public class ChatService
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly ChatAppContext _context;
        private readonly UserService _userService;

        public ChatService(IHubContext<ChatHub> hubContext, ChatAppContext context, UserService userService)
        {
            _hubContext = hubContext;
            _context = context;
            _userService = userService;
        }

        public async Task<Message> CreateMessageAsync(string fromUserEmail, string toUserEmail, string text)
        {

            var fromUser = await _userService.GetUserByEmailAsync(fromUserEmail);
            var toUser = await _userService.GetUserByEmailAsync(toUserEmail);

            var message = new Message
            {
                Text = text,
                Recipient = toUser,
                Sender = fromUser,
                RecipientId = toUser.Id,
                SenderId = fromUser.Id

            };
            var result = await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<List<Message>> getAllMessagesAsync(int recipientId, int senderId)
        {
            var result = await _context.Messages.Where(x => (x.SenderId == senderId && x.RecipientId == recipientId) || (x.SenderId == recipientId && x.RecipientId == senderId)).ToListAsync();

            return result;
        }
    }
}
