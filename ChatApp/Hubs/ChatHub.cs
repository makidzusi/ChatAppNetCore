using ChatApp.DataAccess;
using ChatApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;

namespace ChatApp.Hubs
{

    [Authorize]
    public class ChatHub : Hub
    {
        private List<String?> OnlineUsers  = new List<String?>();

        private readonly ChatService _chatService;

        public ChatHub(ChatService chatService) {
            _chatService= chatService;
        }
        public async Task Send(string message, string to)
        {
            try
            {
                if (Context.UserIdentifier is string userName)
                {
                    await _chatService.CreateMessageAsync(userName,to, message);
                    await Clients.User(to).SendAsync("Receive", message, userName);
                    await Clients.Caller.SendAsync("Receive", message, userName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
      
        }

        public override async Task OnConnectedAsync()
        {
            OnlineUsers.Add(Context.UserIdentifier);
            await Clients.All.SendAsync("Notify", $"Приветствуем {Context.UserIdentifier}");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            OnlineUsers.Remove(Context.UserIdentifier);
            await base.OnDisconnectedAsync(exception);
        }
    }

}

