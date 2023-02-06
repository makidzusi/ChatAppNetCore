using ChatApp.DataAccess;
using ChatApp.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Services
{
    public class ChatService
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly ChatAppContext _context;
        public ChatService(IHubContext<ChatHub> hubContext, ChatAppContext context) { 
            _hubContext= hubContext;
            _context= context;
        }

        public async Task CreateMessageAsync()
        {

        }
    }
}
